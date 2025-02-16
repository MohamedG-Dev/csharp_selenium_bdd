using Newtonsoft.Json.Linq;


namespace CsharpSeleniumTestFramework.utilities
{
    public class JsonReader
    {

        public string extractData(String tokenName)
        {

            String testData = File.ReadAllText("TestData/testData.json");
            var jsonObject = JToken.Parse(testData);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public String[] extractDataArray(String tokenName)
        {
            String testData = File.ReadAllText("TestData/testData.json");
            var jsonObject = JToken.Parse(testData);
            IList<String> productList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();
        }
    }
}
