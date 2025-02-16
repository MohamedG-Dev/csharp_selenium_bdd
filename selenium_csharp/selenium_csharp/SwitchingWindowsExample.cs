using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class SwitchingWindowsExample
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
        driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

    }

    [Test]
    public void SwitchWindowsExamples()
    {
        String expectedEmailId = "mentor@rahulshettyacademy.com";
        driver.FindElement(By.ClassName("blinkingText")).Click();
        //return type of WindowHandles is string array
        Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));
        String parentWindowName = driver.WindowHandles[0];
        String childWindowName = driver.WindowHandles[1];
        //switching to the child window
        driver.SwitchTo().Window(childWindowName);
        //printing an element's text from the child window
        TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
        String text = driver.FindElement(By.CssSelector(".red")).Text;
        String[] splittedText = text.Split("at");
        String[] trimmedString = splittedText[1].Trim().Split(" ");
        Assert.That(trimmedString[0], Is.EqualTo(expectedEmailId));
        //Switching to the parent window
        driver.SwitchTo().Window(parentWindowName);
        driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
        Thread.Sleep(3000);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}
