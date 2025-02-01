using OpenQA.Selenium;

namespace SpecFlowProject1.Pages
{
    public class YoutubeHomePage
    {
        IWebDriver driver;
        By searchTextBox = By.XPath("//input[@name='search_query']");
        public YoutubeHomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public ResultPage enterToSearchText(string text)
        {
            driver.FindElement(searchTextBox).SendKeys(text);
            driver.FindElement(searchTextBox).SendKeys(Keys.Enter);
            return new ResultPage(driver);
        }
    }
}
