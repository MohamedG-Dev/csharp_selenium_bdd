using OpenQA.Selenium;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinition
    {
        private IWebDriver driver;
        public Feature1StepDefinition(IWebDriver driver)
        {
            this.driver = driver;
        }


        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser()
        {
            //driver=new ChromeDriver();
            //driver.Manage().Window.Maximize();
        }

        [When(@"navigate to the youtube")]
        public void WhenNavigateToTheYoutube()
        {
            driver.Url = "https://www.youtube.com/";
            Thread.Sleep(4000);
        }

        [Then(@"search for the TestersTalk")]
        public void ThenSearchForTheTestersTalk()
        {
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys("TestersTalk");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            //driver.Close();
        }
        [Then(@"search for the Testing course")]
        public void ThenSearchForTheTestingCourse()
        {
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys("Testing Course");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            //driver.Close();
        }

    }
}
