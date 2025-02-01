using OpenQA.Selenium;

namespace SpecFlowProject1.Pages
{
    public class ResultPage
    {
        IWebDriver driver;
        By channelName = By.LinkText("Testers Talk");
        public ResultPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public ChannelPage clickOnChannel()
        {
            driver.FindElement(channelName).Click();
            return new ChannelPage(driver);
        }

    }
}
