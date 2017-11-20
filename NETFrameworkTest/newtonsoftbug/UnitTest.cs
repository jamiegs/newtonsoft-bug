using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit.Abstractions;

namespace newtonsoftbug
{
    public class TestClass
    {
        private readonly ITestOutputHelper output;

        public TestClass(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class SerializeObject
        {
            public Dictionary<string, string> Data { get; set; }
        }

        private class KeepCapitalCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                return propertyName.ToUpperInvariant() == propertyName
                    ? propertyName
                    : base.ResolvePropertyName(propertyName);
            }
        }

        [Fact]
        public void TestContractSerializer()
        {
            var obj = new SerializeObject();
            obj.Data = new Dictionary<string, string>();
            obj.Data.Add("CAPITAL", "value");
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new KeepCapitalCasePropertyNamesContractResolver(),
            });
            output.WriteLine(json);
            Assert.Equal("{\"data\":{\"CAPITAL\":\"value\"}}", json);
        }
    }
}
