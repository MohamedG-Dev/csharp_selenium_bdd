using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace CsharpSeleniumTestFramework.pageObjects
{
    public class DeliveryAndPurchasePage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryField;

        [FindsBy(How = How.CssSelector, Using = ".suggestions ul li a")]
        private IList<IWebElement> suggestionsDropDown;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement agreeCheckBox;

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement purchaseButton;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successAlertMessage;

        private By suggestions = By.CssSelector(".suggestions");

        public DeliveryAndPurchasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IWebElement GetCountryField()
        {
            return countryField;
        }

        public void WaitForCountriesSuggestionsLoad()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(suggestions));
        }

        public IList<IWebElement> GetSuggestionsDropDown()
        {
            return suggestionsDropDown;
        }

        public void IAgreeCheckBoxClick()
        {
            agreeCheckBox.Click();
        }

        public void PurchaseButtonClick()
        {
            purchaseButton.Click();
        }

        public String GetSuccessMessage()
        {
            return successAlertMessage.Text;
        }
    }
}
