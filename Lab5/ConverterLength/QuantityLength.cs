using System;
using Paprotski.Quantity;

namespace ConverterLength
{
    [Quantity(Author = "Edward", QuanityType = "Length")]
    public class QuantityLength : IQuantity
    {
        #region Private Member Variables 

        private readonly string[] lengthTable;

        #endregion 

        #region Ctors

        public QuantityLength()
        {
            lengthTable = new [] { "мм", "см", "дм", "м", "км" };
        }

        #endregion

        #region Public Method

        public double ConvertTo(double initiaValue, string toType, string fromType)
        {
            var firstValue = Array.IndexOf(this.lengthTable, toType.ToLower());
            var secondValue = Array.IndexOf(this.lengthTable, fromType.ToLower());
            var exchangeValue = ExchangeQuantityLengthStore.Exchange(firstValue, secondValue);

            return exchangeValue * initiaValue;
        }

        public override string ToString()
        {
            return string.Join(",", lengthTable);
        }

        #endregion
    }
}
