using PriceLib;
using System;
using System.Configuration;

namespace PriceConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainInner();
        }

        static void MainInner()
        {
            ExecutionEngineHkex.LoadConfig();

            var ee = new ExecutionEngineHkex();
            Logger.Info("Start. . .");
            try
            {
                if (ExecutionEngineHkex.CrawlStockList)
                {
                    ee.InitWebDriver();
                    ee.OpenStockListPage();
                    ee.LoadAllStocksInBrowser();
                    ee.ParseAllStocksInBrowser();
                    ee.StoreStockListToFile();
                    ee.QuitWebDriver();
                }

                ee.LoadStockListFrmFile();
                var dateStr = DateTime.Now.ToString("yyyy-MM-ddtt").ToUpper();
                ee.LoadStockPriceListFrmFile(dateStr);
                if (ee.StockPriceList.Count == 0)
                {
                    ee.InitStockPriceListFrmStockList();
                    ee.StoreStockPriceListToFile(dateStr);
                }
                ee.FetchData4StockPriceList(dateStr);

                Logger.Info("ended. . .");
            }
            catch (Exception ex)
            {
                Logger.Error("Exception: {0}, stackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }
    } // class
} // namespace
