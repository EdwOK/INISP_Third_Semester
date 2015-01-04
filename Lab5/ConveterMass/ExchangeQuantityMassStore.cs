namespace ConveterMass
{
    using System;

    public static class ExchangeQuantityMassStore
    {
        #region Private Member Variables

        private readonly static double[,] QuantityMassTable =
            {
                {1, 0.001, 0.000001, 1e-8, 1e-9}, 
                {1000, 1, 0.001, 0.00001, 0.000001},
                {1000000, 1000, 1, 0.01, 0.001},
                {100000000, 100000, 100, 1, 0.1}, 
                {1000000000, 1000000, 1000, 10, 1}
            };

        #endregion

        #region Public Methods

        public static double Exchange(int toPosition, int fromPosition)
        {
            try
            {
                return QuantityMassTable[toPosition, fromPosition];
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        #endregion
    }
}
