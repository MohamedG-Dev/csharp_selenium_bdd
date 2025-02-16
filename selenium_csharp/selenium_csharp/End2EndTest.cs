using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class End2EndTest
{
    private IWebDriver driver;
    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

    }

    [Test]
    public void End2EndFlow()
    {
        String[] expectedProducts = { "iphone X", "Blackberry" };
        String[] actualProducts = new string[2];
        driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
        driver.FindElement(By.Name("password")).SendKeys("learning");
        driver.FindElement(By.XPath("//div[@class='form-group']/child::label[@class='text-info']//input[@type='checkbox']")).Click();
        IWebElement signInButton = driver.FindElement(By.XPath("//input[@id='signInBtn']"));
        signInButton.Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        IList<IWebElement> productContainer = driver.FindElements(By.TagName("app-card"));
        foreach (IWebElement product in productContainer)
        {
            if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
            {
                product.FindElement(By.CssSelector(".card-footer button")).Click();
            }
            TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
        }
        driver.FindElement(By.PartialLinkText("Checkout")).Click();
        IList<IWebElement> checkoutProducts = driver.FindElements(By.CssSelector("h4 a"));
        for (int i = 0; i < checkoutProducts.Count; i++)
        {
            actualProducts[i] = checkoutProducts[i].Text;
        }
        Assert.That(actualProducts, Is.EqualTo(expectedProducts));
        driver.FindElement(By.XPath("//button[contains(text(),'Checkout')]")).Click();
        driver.FindElement(By.Id("country")).SendKeys("ind");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".suggestions")));
        IList<IWebElement> suggestions = driver.FindElements(By.CssSelector(".suggestions ul li a"));
        foreach (IWebElement suggestion in suggestions)
        {
            if (suggestion.Text.Equals("India"))
            {
                suggestion.Click();
                break;
            }
        }
        driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
        driver.FindElement(By.CssSelector("[value='Purchase']")).Click();
        String successMessage = driver.FindElement(By.CssSelector(".alert-success")).Text;
        StringAssert.Contains("Success", successMessage);
        Thread.Sleep(3000);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}
