using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  ApiData;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaxJarApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public  void TestMethod1()
        {
            int zip = 90404;
            Program data = new Program();
            var result =   data.GetTaxesByLocation("https://api.taxjar.com/v2/rates/" + zip);

            var content = result.content;
            Assert.AreEqual("OK", result.HtttpSuccessCode);
            Assert.AreEqual("OK", result.Phrase);

            
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            var data = new TaxJarData()
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                amount = 15,
                shipping = 1.5
            };
            Program model = new Program();
            var result = model.PostTaxesForOrders("https://api.taxjar.com/v2/taxes", data).Result;
            Assert.AreEqual("OK", result.HtttpSuccessCode);
            Assert.AreEqual("OK", result.Phrase);

        }
    }
}
