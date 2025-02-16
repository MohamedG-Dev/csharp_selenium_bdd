using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace CsharpSeleniumTestFramework.utilities
{
    public class BaseTest
    {
        //Extent report setup
        public ExtentReports extentReports;
        public ExtentTest extentTest;
        [OneTimeSetUp]
        public void SetUpReporting()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirecotry = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirecotry + "//index.html";
            var sparkReporter = new ExtentSparkReporter(reportPath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(sparkReporter);
            extentReports.AddSystemInfo("hostname", "dev");
            extentReports.AddSystemInfo("Environment", "QA");
        }
        //public IWebDriver driver;
        //Thread safe
        public ThreadLocal<IWebDriver> driver = new();
        //Below line is for reading data from the terminal/powershell
        String browserName;
        [SetUp]
        public void Setup()
        {
            //entry point for report
            extentTest = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            browserName = TestContext.Parameters["browserName"];
            //by default the configurationmanager looks for a file with .config extension
            //The below code is to fetch the browser value from the App.config file
            //String browser = ConfigurationManager.AppSettings["browser"];
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    throw new Exception("Invalid Browser Name" + browserName);
            }
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testLogs = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                extentTest.Fail("Test Failed", captureScreenShot(driver.Value, fileName));
                extentTest.Log(Status.Fail, "Test Failed with log Trace:" + testLogs);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                extentTest.Pass("Test Passed", captureScreenShot(driver.Value, fileName));
            }

            extentReports.Flush();
            driver.Value.Quit();
        }

        public AventStack.ExtentReports.Model.Media captureScreenShot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot takescreenshot = (ITakesScreenshot)driver;
            var screenshot = takescreenshot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }
    }
}
