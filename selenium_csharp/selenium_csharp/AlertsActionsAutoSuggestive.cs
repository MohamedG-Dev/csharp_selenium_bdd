using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace selenium_csharp;

public class AlertsActionsAutoSuggestive
{
    private IWebDriver driver;
    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
    }

    [Test]
    public void AlertExample()
    {
        //send a value into edit box before viewing the alert
        driver.FindElement(By.CssSelector("#name")).SendKeys("Thomas");

        //click on confirm button
        driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();

        //Switch to Alert and get Text of the Alert
        String alertText = driver.SwitchTo().Alert().Text;
        //print the text of alert box
        TestContext.Progress.WriteLine(alertText);

        //Assert to check the value entered is available in the alert msg box
        //StringAssert is a class from NUnit
        StringAssert.Contains("Thomas", alertText);

        //Accept/OK to click on Alert
        driver.SwitchTo().Alert().Accept();

        //SendKeys is the method that'll help us type if there are any edit boxes in the alert
        //driver.SwitchTo().Alert().SendKeys("Hello");

        //To click on Cancel or dismiss button in the alert. Use the following code:
        //driver.SwitchTo().Alert().Dismiss();
    }

    [Test]
    public void HandlingDynamicDropDowns()
    {
        ArrayList arrayList = new ArrayList();
        driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
        //Added wait to make the dropdown visible
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".ui-menu")));
        //Click on the India DropDown from the dropdown results
        IList<IWebElement> menuItems = driver.FindElements(By.CssSelector(".ui-menu-item div"));
        foreach (IWebElement menuItem in menuItems)
        {
            if (menuItem.Text.Equals("India"))
            {
                menuItem.Click();
            }
        }
        Thread.Sleep(3000);
        //Run Time - dynamic fields we need to use GetAttriubute()method to fetch the value
        String selectedValue = driver.FindElement(By.Id("autocomplete")).GetAttribute("value");
        TestContext.Progress.WriteLine("The selected value from the dynamic drop down is: " + selectedValue);
    }

    [Test]
    public void ActionsClassExamples()
    {
        driver.Url = "https://rahulshettyacademy.com/practice-project";
        Actions actions = new Actions(driver);
        actions.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
        //driver.FindElement(By.XPath("//ul[@class='dropdown-menu']//child::a[text()='About us']")).Click();
        actions.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']//child::a[text()='About us']"))).Click().Perform();

        Thread.Sleep(3000);
    }

    [Test]
    public void DragAndDropActionsClassExample()
    {
        driver.Url = "https://demoqa.com/dragabble";
        Actions action = new Actions(driver);
        action.MoveToElement(driver.FindElement(By.XPath("//a[text()='Axis Restricted']"))).Click().Perform();
        action.DragAndDrop(driver.FindElement(By.Id("restrictedX")), driver.FindElement(By.Id("restrictedY"))).Perform();
        Thread.Sleep(3000);
    }
    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}
