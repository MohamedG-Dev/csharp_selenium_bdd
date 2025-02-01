using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Pages;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class PageObjectModelExampleStepDefinitions
    {
        private IWebDriver driver;
        private YoutubeHomePage homePage;
        private ResultPage resultPage;
        private ChannelPage channelPage;
        public PageObjectModelExampleStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }
        [Given(@"Navigate to the Youtube website")]
        public void GivenNavigateToTheYoutubeWebsite()
        {
            driver.Url = "https://www.youtube.com/";
            Thread.Sleep(2000);
        }

        [When(@"Search for the '([^']*)' in youtube")]
        public void WhenSearchForTheTestersTalkInYoutube(String testersTalk)
        {
            homePage = new YoutubeHomePage(driver);
            resultPage = homePage.enterToSearchText(testersTalk);
            Thread.Sleep(3000);

        }

        [When(@"Navigate to the Channerl Page")]
        public void WhenNavigateToTheChannerlPage()
        {
            channelPage = resultPage.clickOnChannel();
            Thread.Sleep(3000);
        }

        [Then(@"Verify the title of the page (.*)")]
        public void ThenVerifyTheTitleOfThePage(string pageTitle)
        {
            var titleOfPage = channelPage.getTitle();
            Assert.That(titleOfPage, Does.Contain(pageTitle));
        }
    }
}
