using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class Locators
{
    private IWebDriver driver;
    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        //Implicit Waits are declared to global wait
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
    }

    [Test]
    public void Test1()
    {
        //find an element using Id locator and enter a value
        driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy ");
        //command to Clear an edit field
        driver.FindElement(By.Id("username")).Clear();
        driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy ");
        //command to identify using Name locator
        driver.FindElement(By.Name("password")).SendKeys("learning");
        //click on checkbox using xpath
        driver.FindElement(By.XPath("//div[@class='form-group']/child::label[@class='text-info']//input[@type='checkbox']")).Click();
        // css for the check box to click - .text-info span:nth-child(1)
        //driver.FindElement(By.CssSelector(".text-info span:nth-child(1)")).Click();
        //command to find an element using xpath and click on it
        IWebElement signInButton = driver.FindElement(By.XPath("//input[@id='signInBtn']"));
        signInButton.Click();
        //Thread.Sleep(3000);
        //Explicit Wait
        WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(signInButton, "Sign In"));
        //command to find an element using ClassName and get the text of it
        string errorText = driver.FindElement(By.ClassName("alert-danger")).Text;
        TestContext.Progress.WriteLine("Error Message: " + errorText);
        //command to identify an element using LinkText locator
        //And get the attribute of the element
        IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
        String linkTextAttribute = link.GetAttribute("href");
        String expectedUrl = "https://rahulshettyacademy.com/documents-request";
        //verify the link is as expected one
        Assert.That(linkTextAttribute, Is.EqualTo(expectedUrl));


    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}
