using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;//SelectElement package
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class FunctionalTest
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
    public void dropdownSelection()
    {
        Thread.Sleep(3000);
        IWebElement dropDown = driver.FindElement(By.CssSelector("select.form-control"));
        SelectElement selectElement = new SelectElement(dropDown);
        selectElement.SelectByText("Teacher");
        Thread.Sleep(3000);
        selectElement.SelectByValue("consult");
        Thread.Sleep(3000);
        selectElement.SelectByIndex(1);
        Thread.Sleep(3000);

    }

    [Test]
    public void selectRadioButton()
    {
        Thread.Sleep(3000);
        IList<IWebElement> radioButtons = driver.FindElements(By.XPath("//input[@type='radio']"));
        foreach (IWebElement radionButton in radioButtons)
        {
            if (radionButton.GetAttribute("value").Equals("user"))
            {
                radionButton.Click();
            }
        }
        //Thread.Sleep(3000);
        IWebElement okButton = driver.FindElement(By.Id("okayBtn"));
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(okButton));
        okButton.Click();
        bool selectedValue = driver.FindElement(By.Id("usertype")).Selected;
        Assert.That(selectedValue, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}
