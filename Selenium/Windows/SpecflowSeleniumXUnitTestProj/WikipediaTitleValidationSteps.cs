using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Xunit;

namespace SpecflowSeleniumXUnitTestProj
{
    [Binding]
    public class WikipediaTitleValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly IWebDriver _webDriver;

        public WikipediaTitleValidationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = scenarioContext["WEB_DRIVER"] as IWebDriver;
        }

        [Given(@"I have navigated to the ""(.*)"" page on Wikipedia")]
        public void GivenIHaveNavigatedToThePageOnWikipedia(string subject)
        {
            this._webDriver.Url = $"https://en.wikipedia.org/wiki/{subject}";
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("p-logo")));
        }
        
        [Then(@"the title of the page should be ""(.*)""")]
        public void ThenTheTitleOfThePageShouldBe(string title)
        {
            Assert.Equal(title, _webDriver.Title);
        }
    }
}
