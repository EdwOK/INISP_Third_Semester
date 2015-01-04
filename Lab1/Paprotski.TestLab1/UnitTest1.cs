using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paprotski.Lab1;

namespace Paprotski.TestLab1
{
    [TestClass]
    public class UnitCurrencyTest
    {
        [TestMethod]
        public void TestCreateObject()
        {
            var price = new Price(2500, CurrencyCode.USD);
        }

        [TestMethod]
        public void TestParseObject()
        {
            Price price1;
            var price2 = Price.Parse("      100   BYR      ");
            var isPrice = Price.TryParse("     999 USD      ", out price1);

            Assert.AreEqual(price2.Value, 100);
            Assert.AreEqual(isPrice, true);
        }

        [TestMethod]
        public void TestComparisonOperationsObjects()
        {
            var price1 = new Price(1300, CurrencyCode.EUR);
            var price2 = new Price(2000, CurrencyCode.USD);

            Assert.AreEqual(price1.Equals(price1), true);
            Assert.AreEqual(price1.Equals(null), false);
            Assert.AreEqual(price1.Equals(price2) == price2.Equals(price1), true);

            Assert.AreEqual(price1 > price2, false);
            Assert.AreEqual(price1 < price2, true);
            Assert.AreEqual(price1 == price2, false);
            Assert.AreEqual(price1 != price2, true);
            
            Assert.AreEqual(price1.CompareTo(price2), -1);
        }

        [TestMethod]
        public void TestMathOperationObjects()
        {
            var price1 = new Price(50, CurrencyCode.EUR);
            var price2 = new Price(5000, CurrencyCode.RUB);
            var price3 = price1 + price2;
            var price4 = price2 - price1; 

            Console.WriteLine(price3 + " " + price4);
        }

        [TestMethod]
        public void TestStringFormatObject()
        {
            var price1 = new Price(2500, CurrencyCode.USD);
            var price2 = new Price(10490, CurrencyCode.BYR);

            Assert.AreEqual(string.Format("{0:EUR} {1:USD}", price1, price2), "1933,975 EUR 1 USD");
            Assert.AreEqual(string.Format("{0:BYR} {1:RUB}", price1, price2), "26225000 BYR 37,5542 RUB");
        }

    }
}
