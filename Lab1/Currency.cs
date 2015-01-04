namespace Paprotski.Lab1
{
    using System;

    public class Currency
    {
        #region Private Member Variables
        
        private static readonly string[] DescriptionTable =
            {
                "It is the official currency of the United States and its overseas territories",
                "The currency used by the Institutions of the European Union and is the official currency of the eurozone",
                "The currency of the Republic of Belarus",
                "The currency of the Russian Federation and the two partially recognized republics of Abkhazia and South Ossetia"
            };

        private static readonly string[] SymbolTable = { "$", "€", "б.р.", "р." };

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        public Currency(CurrencyCode currencyCode)
        {
            this.Code = currencyCode;
            this.Name = currencyCode.ToString();
            this.Description = DescriptionTable[(int)currencyCode];
            this.Symbol = SymbolTable[(int)currencyCode];
            this.IsCoin = currencyCode != CurrencyCode.BYR;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the currency code.
        /// </summary>
        public CurrencyCode Code { get; private set; }

        /// <summary>
        /// Gets the currency name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the currency description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the currency symbol.
        /// </summary>
        public string Symbol { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the given currency coins
        /// </summary>
        public bool IsCoin { get; private set; }

        #endregion

        #region Public Methods

        #endregion 
    }
}
