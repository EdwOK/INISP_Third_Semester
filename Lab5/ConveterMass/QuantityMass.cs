using System;
using System.Text;
using Paprotski.Quantity;

namespace ConveterMass
{
    [Quantity(Author = "Edward", QuanityType = "Mass")]
    public class QuantityMass : IQuantity
    {
        #region Private Member Variables 

        private readonly string[] massTable;

        #endregion 

        #region Ctors

        public QuantityMass()
        {
            massTable = new [] { "мг", "г", "кг", "ц", "т" };
        }

        #endregion

        #region Public Method

        public double ConvertTo(double initiaValue, string toType, string fromType)
        {
            var firstValue = Array.IndexOf(this.massTable, toType.ToLower());
            var secondValue = Array.IndexOf(this.massTable, fromType.ToLower());
            var exchangeValue = ExchangeQuantityMassStore.Exchange(firstValue, secondValue);

            return exchangeValue * initiaValue;
        }

        public override string ToString()
        {
            return string.Join(",", massTable); 
        }

        #endregion
    }
}
