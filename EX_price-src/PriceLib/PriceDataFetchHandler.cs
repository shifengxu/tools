using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PriceLib
{
    public class PriceDataFetchHandler
    {
        IWebDriver driver;

        ExecutionEngineHkex ee;

        public PriceDataFetchHandler(ExecutionEngineHkex ee)
        {
            this.ee = ee;

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
        }

        public void QuitWebServer()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        public void HandleStockPrices()
        {
            StockDayEndPrice stockPrice;
            while((stockPrice = ee.NextEmptyStockPrice()) != null)
            {
                this.FetchData4StockPrice(stockPrice);
            }
        }

        public StockDayEndPrice FetchData4StockPrice(StockDayEndPrice stockPrice)
        {
            var url = ExecutionEngineHkex.UrlStockTemplate.Replace("{StockCode}", stockPrice.Code + "");
            try
            {
                driver.Url = url;
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span.col_last")));
                Thread.Sleep(1000);
                IWebElement elem;
                elem = driver.FindElement(By.CssSelector("dt.col_open"));
                stockPrice.OpenPrice = elem.Text;
                elem = driver.FindElement(By.CssSelector("span.col_last"));
                stockPrice.ClosePrice = elem.Text;
                elem = driver.FindElement(By.CssSelector("dt.col_volume"));
                stockPrice.Volume = elem.Text;
                elem = driver.FindElement(By.CssSelector("td.col_high"));
                stockPrice.HighPrice = elem.Text;
                elem = driver.FindElement(By.CssSelector("td.col_low"));
                stockPrice.LowPrice = elem.Text;

                return stockPrice;
            }
            catch (Exception ex)
            {
                Logger.Error("url: {0}. name: {1}. Exception: {2}, {3}", url, stockPrice.Name, ex.Message, ex.StackTrace);
                return null;
            }
        }

    }
}
