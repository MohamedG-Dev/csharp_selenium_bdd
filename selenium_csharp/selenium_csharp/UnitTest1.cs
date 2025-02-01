namespace selenium_csharp
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup method is executed");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("Test method is executed");
        }
        [TearDown]
        public void TearDown()
        {
            TestContext.Progress.WriteLine("TearDown method is executed");
        }
    }
}