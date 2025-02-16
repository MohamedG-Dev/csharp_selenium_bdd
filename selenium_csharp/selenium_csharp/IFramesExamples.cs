using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class IFramesExamples
{
    IWebDriver driver;
    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
    }

    [Test]
    public void HandlingFrames()
    {
        IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
        //scroll to IFrame
        IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
        javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);
        //ways to switch into frame: id,name and index
        driver.SwitchTo().Frame("courses-iframe");
        driver.FindElement(By.LinkText("All Access Plan")).Click();
        TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        driver.SwitchTo().DefaultContent();
        TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        Thread.Sleep(3000);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}
