using OpenQA.Selenium;

namespace SpecFlowProject1.Pages
{
    public class ChannelPage
    {
        IWebDriver driver;
        public ChannelPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string getTitle()
        {
            return driver.Title;
        }
    }
}
