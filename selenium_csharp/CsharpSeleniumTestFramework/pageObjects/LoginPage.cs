using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CsharpSeleniumTestFramework.pageObjects
{
    public class LoginPage
    {
        //Driver assignment
        private IWebDriver driver;

        //PageObject factory design Pattern
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group']/child::label[@class='text-info']//input[@type='checkbox']")]
        private IWebElement checkBox;

        [FindsBy(How = How.XPath, Using = "//input[@id='signInBtn']")]
        private IWebElement signInButton;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ShoppingProductsPage validLogin(String UserName, String Password)
        {
            username.SendKeys(UserName);
            password.SendKeys(Password);
            signInButton.Click();
            return new ShoppingProductsPage(driver);
        }
    }
}
