using CsharpSeleniumTestFramework.pageObjects;
using CsharpSeleniumTestFramework.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CsharpSeleniumTestFramework.tests
{
    //2. run all test methods in one class parallel
    //[Parallelizable(ParallelScope.Children)]
    //3. run all test files in project parallel
    [Parallelizable(ParallelScope.Self)]
    public class Tests : BaseTest
    {

        //fetching data from a method - Parameterization
        //to run test in a group from command line, use - "Category" Keyword
        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("Regression")]
        //Parametization
        //[TestCase("rahulshettyacademy", "learning")]//Positive scenario
        //[TestCase("rahulshett", "learning")]//Negative scenario

        //3Ways to run tests in parallel
        //1. run all data sets of test method in parallel
        //[Parallelizable(ParallelScope.All)]
        //2.run all test methods in one class parallel
        //3. run all test files in project parallel
        public void End2EndFlow(String username, String password, String[] expectedProducts)
        {
            // string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];
            LoginPage loginPage = new LoginPage(getDriver());
            ShoppingProductsPage shoppingProductsPage = loginPage.validLogin(username, password);
            shoppingProductsPage.waitForPageToDisplay();
            IList<IWebElement> productContainer = shoppingProductsPage.getProductContainer();
            foreach (IWebElement product in productContainer)
            {
                if (expectedProducts.Contains(product.FindElement(shoppingProductsPage.getCardTitle()).Text))
                {
                    product.FindElement(shoppingProductsPage.getAddCartButton()).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(shoppingProductsPage.getCardTitle()).Text);
            }
            CheckOutPage checkOutPage = shoppingProductsPage.ClickCheckOutButton();
            IList<IWebElement> checkoutProducts = checkOutPage.GetCheckOutProducts();
            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                actualProducts[i] = checkoutProducts[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            DeliveryAndPurchasePage devliveryAndPurchase = checkOutPage.ClickCheckOutButton();
            devliveryAndPurchase.GetCountryField().SendKeys("ind");
            devliveryAndPurchase.WaitForCountriesSuggestionsLoad();
            IList<IWebElement> suggestions = devliveryAndPurchase.GetSuggestionsDropDown();
            foreach (IWebElement suggestion in suggestions)
            {
                if (suggestion.Text.Equals("India"))
                {
                    suggestion.Click();
                    break;
                }
            }
            devliveryAndPurchase.IAgreeCheckBoxClick();
            devliveryAndPurchase.PurchaseButtonClick();
            string successMessage = devliveryAndPurchase.GetSuccessMessage();
            StringAssert.Contains("Success", successMessage);
            Thread.Sleep(3000);
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("invalidUsername"), getDataParser().extractData("invalidPassword"), getDataParser().extractDataArray("products"));
        }

        [Test, Category("Smoke")]
        public void LocatorsTest()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy ");
            driver.Value.FindElement(By.Id("username")).Clear();
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy ");
            driver.Value.FindElement(By.Name("password")).SendKeys("learning");
            driver.Value.FindElement(By.XPath("//div[@class='form-group']/child::label[@class='text-info']//input[@type='checkbox']")).Click();
            IWebElement signInButton = driver.Value.FindElement(By.XPath("//input[@id='signInBtn']"));
            signInButton.Click();
            WebDriverWait explicitWait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(signInButton, "Sign In"));
            string errorText = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine("Error Message: " + errorText);
        }

    }
}