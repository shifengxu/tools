using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;

namespace PriceLib
{
    public class ExecutionEngineHkex : ExecutionEngine 
    {
        public static string UrlEqulities { get; set; } = @"https://www.hkex.com.hk/Market-Data/Securities-Prices/Equities?sc_lang=en";

        public static string UrlStockTemplate { get; set; } = @"https://www.hkex.com.hk/Market-Data/Securities-Prices/Equities/Equities-Quote?sym={StockCode}&sc_lang=en";

        public static int BrowserMaxCount { get; set; } = 3;

        public static string StockListFile { get; set; } = @"C:\L\Robin_Han\Crawler\StockListFile.csv";

        public static bool CrawlStockList { get; set; } = false;

        public static string StockPriceListDir { get; set; } = @"C:\L\Robin_Han\Crawler\StockPrice";

        public IList<StockInfo> StockList { get; set; } = new List<StockInfo>();

        public IList<StockDayEndPrice> StockPriceList { get; set; } = new List<StockDayEndPrice>();
        private int StockPriceListIndex = 0;
        private object StockPriceListLocker = new object();

        public static void LoadConfig()
        {
            string x;
            x = ConfigurationManager.AppSettings["HK_UrlEqulities"];
            if (!string.IsNullOrEmpty(x)) ExecutionEngineHkex.UrlEqulities = x;

            x = ConfigurationManager.AppSettings["HK_UrlStockTemplate"];
            if (!string.IsNullOrEmpty(x)) ExecutionEngineHkex.UrlStockTemplate = x;

            x = ConfigurationManager.AppSettings["HK_BrowserMaxCount"];
            if (!string.IsNullOrEmpty(x)) ExecutionEngineHkex.BrowserMaxCount = int.Parse(x);

            x = ConfigurationManager.AppSettings["HK_StockListFile"];
            if (!string.IsNullOrEmpty(x)) ExecutionEngineHkex.StockListFile = x;

            x = ConfigurationManager.AppSettings["HK_CrawlStockList"];
            ExecutionEngineHkex.CrawlStockList = !string.IsNullOrEmpty(x) && x.Equals("true", StringComparison.OrdinalIgnoreCase);

            x = ConfigurationManager.AppSettings["HK_StockPriceListDir"];
            if (!string.IsNullOrEmpty(x)) ExecutionEngineHkex.StockPriceListDir = x;

            Logger.Info("ExecutionEngineHkex.UrlEqulities     :{0}", ExecutionEngineHkex.UrlEqulities);
            Logger.Info("ExecutionEngineHkex.UrlStockTemplate :{0}", ExecutionEngineHkex.UrlStockTemplate);
            Logger.Info("ExecutionEngineHkex.BrowserMaxCount  :{0}", ExecutionEngineHkex.BrowserMaxCount);
            Logger.Info("ExecutionEngineHkex.StockListFile    :{0}", ExecutionEngineHkex.StockListFile);
            Logger.Info("ExecutionEngineHkex.CrawlStockList   :{0}", ExecutionEngineHkex.CrawlStockList);
            Logger.Info("ExecutionEngineHkex.StockPriceListDir:{0}", ExecutionEngineHkex.StockPriceListDir);
        }

        public int InitStockPriceListFrmStockList()
        {
            this.StockPriceList.Clear();
            foreach(var si in StockList)
            {
                var sp = new StockDayEndPrice(si);
                this.StockPriceList.Add(sp);
            }
            return this.StockPriceList.Count;
        }

        public void OpenStockListPage()
        {
            Logger.Info("OpenPage()");
            if (driver == null) this.InitWebDriver();

            driver.Url = UrlEqulities;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("lhkexw-equities")));
        }

        #region Stock List
        public void LoadAllStocksInBrowser()
        {
            Logger.Info("LoadAllStocksInBrowser()");
            //driver.Manage().Window.Size = new System.Drawing.Size(1552, 840);  // 2 | setWindowSize | 1552x840 | 

            var cookieClose = driver.FindElement(By.Id("accpet_cookie_btn"));
            if (cookieClose != null)
            {
                cookieClose.Click();
                this.SleepSeconds(1);
            }

            var dropdown = driver.FindElement(By.CssSelector(".loadmore_update")); // dropdown of page size: 20 Items
            if (dropdown.Displayed)
            {
                actions.MoveToElement(dropdown).Click();
                dropdown.Click();
                this.SleepSeconds(1);
                driver.FindElement(By.CssSelector(".select_item:nth-child(3) > .items")).Click(); // select 100 Items
                this.SleepSeconds(1);
            }

            var loadMoreElem = driver.FindElement(By.CssSelector(".load"));
            while (loadMoreElem != null && loadMoreElem.Displayed)
            {
                actions.MoveToElement(loadMoreElem).Click();
                Logger.Info("Load More clicked");
                loadMoreElem.Click();
                SleepSeconds(3);
                loadMoreElem = driver.FindElement(By.CssSelector(".load"));
            }
        }

        public IList<StockInfo> ParseAllStocksInBrowser()
        {
            Logger.Info("ParseAllStocksInBrowser()");
            StockList.Clear();
            var trList = driver.FindElements(By.CssSelector("table.table_equities tr.datarow"));
            Logger.Info("Data row count:{0}", trList.Count);
            foreach(var tr in trList)
            {
                var codeStr = tr.FindElement(By.CssSelector("td.code a")).Text;
                var nameStr = tr.FindElement(By.CssSelector("td.name a")).Text;
                var spanList = tr.FindElements(By.CssSelector("td.code span.suspend"));
                var isSuspended = spanList.Count > 0 && spanList[0].Displayed && spanList[0].Text.Equals("Suspended", StringComparison.OrdinalIgnoreCase);
                int code;
                if (!int.TryParse(codeStr, out code))
                {
                    Logger.Error("failed to parse code. Code:{0}, name:{1}", codeStr, nameStr);
                }
                var si = new StockInfo(code, nameStr, isSuspended);
                StockList.Add(si);
            }
            Logger.Info("StockList.Count: " + StockList.Count);
            return StockList;
        }

        public string StoreStockListToFile(string filepath = null)
        {
            Logger.Info("StoreStockListToFile()");
            filepath ??= StockListFile;
            using (var file = new StreamWriter(filepath))
            {
                foreach (var s in StockList)
                {
                    file.WriteLine(s.ToString());
                }
            }
            return filepath;
        }

        public int LoadStockListFrmFile(string filepath = null)
        {
            Logger.Info("LoadStockListFrmFile()");
            filepath ??= StockListFile;
            StockList.Clear();
            using (var file = new StreamReader(filepath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    StockList.Add(StockInfo.Parse(line));
                }
            }

            return StockList.Count;
        }
        #endregion

        #region Stock Price List
        public int LoadStockPriceListFrmFile(string dateStr)
        {
            Logger.Info("LoadStockPriceListFrmFile()");
            StockPriceList.Clear();
            string filepath = Path.Combine(StockPriceListDir, dateStr + ".csv");
            if (!File.Exists(filepath)) return -1;

            using (var file = new StreamReader(filepath))
            {
                string line;
                while((line = file.ReadLine()) != null)
                {
                    StockPriceList.Add(StockDayEndPrice.Parse(line));
                }
            }
            return StockPriceList.Count;
        }

        public string StoreStockPriceListToFile(string dateStr)
        {
            Logger.Info("StoreStockPriceListToFile()");
            string filepath = Path.Combine(StockPriceListDir, dateStr + ".csv");
            using (var file = new StreamWriter(filepath))
            {
                foreach (var s in StockPriceList)
                {
                    file.WriteLine(s.ToString());
                }
            }
            return filepath;
        }

        public StockDayEndPrice NextEmptyStockPrice()
        {
            lock (StockPriceListLocker)
            {
                while (StockPriceListIndex < StockPriceList.Count)
                {
                    var sp = StockPriceList[StockPriceListIndex++];
                    if (!sp.MissingData()) continue;

                    Logger.Debug("NextEmptyStockPrice() {0}/{1}. code:{2}", StockPriceListIndex, StockPriceList.Count, sp.Code);
                    return sp;
                }
                return null;
            }
        }

        public void FetchData4StockPriceList(string dateStr)
        {
            IList<Thread> tList = new List<Thread>();
            for (var i = 0; i < BrowserMaxCount; i++)
            {
                var pdHandler = new PriceDataFetchHandler(this);
                Thread t = new Thread(() => {
                    pdHandler.HandleStockPrices();
                    pdHandler.QuitWebServer();
                });
                tList.Add(t);
            }
            foreach (var t in tList) t.Start();

            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.AutoReset = true;
            timer.Elapsed += (sender, e) =>
            {
                this.StoreStockPriceListToFile(dateStr);
            };
            timer.Enabled = true;
            timer.Start();

            foreach (var t in tList) t.Join();
            this.StoreStockPriceListToFile(dateStr);
        }
        #endregion
    }
}
