using OpenQA.Selenium;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class DataDrivenTestingFeatureStepDefinitions
    {
        private IWebDriver driver;
        public DataDrivenTestingFeatureStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }
        [Then(@"search for '([^']*)'")]
        public void ThenSearchFor(string searchText)
        {
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(searchText);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
        }

        [Then(@"search for (.*)")]
        public void ThenSearchFor(int numberValue)
        {
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(numberValue.ToString());
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
        }

        [Then(@"search with (.*)")]
        public void ThenSearchWithSpecflowByTestersTalk(string searchValue)
        {
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(searchValue);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
        }

        [Then(@"Enter search keyword in Youtube")]
        public void ThenEnterSearchKeywordInYoutube(Table table)
        {
            var searchText = table.CreateSet<SearchKeyTestData>();
            foreach (var textSearch in searchText)
            {
                driver.FindElement(By.XPath("//input[@name='search_query']")).Clear();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(textSearch.searchValue);
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//input[@name='search_query']")).SendKeys(Keys.Enter);
                Thread.Sleep(2000);
            }

        }

    }

    public class SearchKeyTestData
    {
        //Note: searchValue should be as same as mentioned in the feature file, data table keyword
        public string searchValue { get; set; }
    }
}
