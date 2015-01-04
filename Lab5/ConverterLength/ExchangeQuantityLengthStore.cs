namespace ConverterLength
{
    using System;

    public static class ExchangeQuantityLengthStore
    {
        #region Private Member Variables

        private readonly static double[,] QuantityLengthTable =
            {
                {1, 0.1, 0.01, 0.001, 1e-6}, 
                {10, 1, 0.1, 0.01, 1e-5}, 
                {100, 10, 1, 0.1, 1e-4}, 
                {1000, 100, 10, 1, 0.001}, 
                {1000000, 100000, 10000, 1000, 1} 
            };

        #endregion

        #region Public Methods

        public static double Exchange(int toPosition, int fromPosition)
        {
            try
            {
                return QuantityLengthTable[toPosition, fromPosition];
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        #endregion
    }
}
