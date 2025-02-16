using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace CsharpSeleniumTestFramework.pageObjects
{
    public class ShoppingProductsPage
    {
        private IWebDriver driver;
        //driver.FindElement(By.PartialLinkText("Checkout"))
        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement CheckOutButton;

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> CardsContainer;

        By checkoutButton = By.PartialLinkText("Checkout");
        By cardTitle = By.CssSelector(".card-title a");
        By addCartButton = By.CssSelector(".card-footer button");
        public ShoppingProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void waitForPageToDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(checkoutButton));
        }

        public IList<IWebElement> getProductContainer()
        {
            return CardsContainer;
        }

        public By getCardTitle()
        {
            return cardTitle;
        }

        public By getAddCartButton()
        {
            return addCartButton;
        }
        public CheckOutPage ClickCheckOutButton()
        {
            CheckOutButton.Click();
            return new CheckOutPage(driver);
        }
    }
}
