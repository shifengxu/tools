using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumXUnitTestProj
{
    [Binding]
    public class TestHooks
    {
        [Before]
        //[BeforeTestRun]
        public void CreateWebDriver(ScenarioContext context)
        {
            // We are using Chrome, but you can use any driver
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            IWebDriver webDriver = new ChromeDriver(options);
            context["WEB_DRIVER"] = webDriver;
        }

        [After]
        //[AfterTestRun]
        public void CloseWebDriver(ScenarioContext context)
        {
            var driver = context["WEB_DRIVER"] as IWebDriver;
            driver.Quit();
        }
    }
}