using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace CsharpSeleniumTestFramework.tests
{
    //3. run all test files in project parallel
    [Parallelizable(ParallelScope.Self)]
    public class SortWebTables
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void SortingWebTables()
        {
            ArrayList beforeSortedList = new ArrayList();
            SelectElement select = new SelectElement(driver.FindElement(By.Id("page-menu")));
            select.SelectByValue("20");
            //Fetch all the product names into arrayList
            IList<IWebElement> listOfProducts = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in listOfProducts)
            {
                beforeSortedList.Add(veggie.Text);
            }

            //printing array list before sorting
            TestContext.Progress.WriteLine("Printing the array List before sorting");
            foreach (String str in beforeSortedList)
            {
                TestContext.Progress.WriteLine(str);
            }

            //Sorting the array list
            beforeSortedList.Sort();

            //printing array list after sorting
            TestContext.Progress.WriteLine("Printing the array list after sorting");
            foreach (String str in beforeSortedList)
            {
                TestContext.Progress.WriteLine(str);
            }
            //Click on fruit name cell to display the sorted veggies column
            driver.FindElement(By.CssSelector("th[aria-label*='fruit name']")).Click();
            ArrayList sortedArrayList = new ArrayList();

            //Fetch sorted products and store in to the sortedArrayList
            IList<IWebElement> sortedProducts = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in sortedProducts)
            {
                sortedArrayList.Add(veggie.Text);
            }

            //Validate the arrayList values are same.
            //Note: beforeSortedList is sorted using the Sort()method of CSharp
            // sortedArrayList this List contains the soted data fetched from screen
            Assert.That(sortedArrayList, Is.EqualTo(beforeSortedList));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}