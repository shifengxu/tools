using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace PriceLib
{
    public class ExecutionEngine
    {
        protected IWebDriver driver = null;
        protected Actions actions = null;

        public IWebDriver InitWebDriver()
        {
            Logger.Info("InitWebDriver()");
            // We are using Chrome, but you can use any driver
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
            actions = new Actions(driver);
            return driver;
        }

        public void QuitWebDriver()
        {
            Logger.Info("QuitWebDriver()");
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        public void Sleep(int milliSeconds)
        {
            Thread.Sleep(milliSeconds);
        }

        public void SleepSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
