namespace Paprotski.Lab1
{
    public static class ExchangeRateStore
    {
        #region Private Member Variables 

        /// <summary>
        /// The table conversion exchange rates. 
        /// </summary>
        private readonly static double[,] RateTable =
            {
                { 1, 0.77359, 10490, 37.59856 }, 
                { 1.29266, 1, 13560, 48.60215 },
                { 0.000095329, 0.000073746, 1, 0.00358 }, 
                { 0.02569, 0.02057, 279, 1 }
            };

        #endregion

        #region Public Methods

        /// <summary>
        /// The get rate.
        /// </summary>
        /// <param name="firstCurrencyCode">
        /// The first code is the current currency exchange.
        /// </param>
        /// <param name="secondCurrencyCode">
        /// The second currency code is the currency of exchange in which to exchange.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public static decimal GetRate(this CurrencyCode firstCurrencyCode, CurrencyCode secondCurrencyCode)
        {
            return (decimal)RateTable[(int)firstCurrencyCode, (int)secondCurrencyCode]; 
        }

        #endregion
    }
}
