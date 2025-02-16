using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CsharpSeleniumTestFramework.pageObjects
{
    public class CheckOutPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkoutProducts;
        //By.XPath("")
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Checkout')]")]
        private IWebElement checkoutButton;

        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IList<IWebElement> GetCheckOutProducts()
        {
            return checkoutProducts;
        }

        public DeliveryAndPurchasePage ClickCheckOutButton()
        {
            checkoutButton.Click();
            return new DeliveryAndPurchasePage(driver);
        }
    }
}
