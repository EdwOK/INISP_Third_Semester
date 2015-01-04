using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Paprotski.Lab1
{
    public class Price : IEquatable<Price>, IComparable<Price>, IFormatProvider, IFormattable
    {
        #region Private Member Variables

        /// <summary>
        /// The value.
        /// </summary>
        private decimal value;

        /// <summary>
        /// The currencies list.
        /// </summary>
        private static readonly List<Currency> CurrenciesList = new List<Currency>()
                                                                    {
                                                                        new Currency(CurrencyCode.USD),
                                                                        new Currency(CurrencyCode.EUR),
                                                                        new Currency(CurrencyCode.BYR),
                                                                        new Currency(CurrencyCode.RUB)
                                                                    };

        #endregion

        #region Private Methods

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        public Price(decimal value, CurrencyCode currencyCode)
        {
            this.Currency = CurrenciesList[(int)currencyCode];
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        public Price()
            : this(0, CurrencyCode.BYR)
        {
        }

        #endregion

        #region Public Properties 

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        private Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public decimal Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value < 0)
                {
                    throw new ArgumentOutOfRangeException("Negetive price!");
                }

                if (Currency.Code == CurrencyCode.BYR)
                {
                    if (value % 100 == 0)
                    {
                        this.value = value != 0 ? value : 100;
                    }
                    else
                    {
                        this.value = Math.Ceiling(value / 100) * 100;
                    }   
                }

                this.value = value;
            }
        }

        #endregion

        #region Public Methods

        public static Price Parse(string s)
        {
            return Parse(s, NumberStyles.Number, CultureInfo.CurrentCulture);
        }

        public static Price Parse(string s, IFormatProvider formatProvider)
        {
            return Parse(s, NumberStyles.Number, formatProvider);
        }

        public static Price Parse(string s, NumberStyles styles)
        {
            return Parse(s, styles, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="s">
        /// The string to parse into an object.
        /// </param>
        /// <param name="styles">
        /// The styles permitted in numeric string arguments.
        /// </param>
        /// <param name="formatProvider">
        /// The format provider for string formatting.
        /// </param>
        /// <returns>
        /// The <see cref="Price"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="FormatException">
        /// </exception>
        public static Price Parse(string s, NumberStyles styles, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("String is empty!");
            }

            decimal cost; 

            foreach (var currency1 in CurrenciesList)
            {
                var indexOf = s.IndexOf(currency1.Symbol, StringComparison.Ordinal);

                if (s.Contains(currency1.Symbol))
                {
                    cost = decimal.Parse(s.Substring(0, indexOf), styles, formatProvider);
                    return new Price(cost, currency1.Code);
                }
            }

            foreach (var currency in Enum.GetNames(typeof(CurrencyCode)))
            {
                var indexOf = s.IndexOf(currency, StringComparison.Ordinal);

                if (s.Contains(currency))
                {
                    cost = decimal.Parse(s.Substring(0, indexOf), styles, formatProvider);
                    return new Price(cost, (CurrencyCode)Enum.Parse(typeof(CurrencyCode), currency));
                }
            }

            throw new FormatException();
        }

        public static bool TryParse(string s, out Price price)
        {
            return TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out price);
        }

        /// <summary>
        /// The try parse.
        /// </summary>
        /// <param name="s">
        /// The string to try parse into an object.
        /// </param>
        /// <param name="styles">
        /// The styles permitted in numeric string arguments.
        /// </param>
        /// <param name="provider">
        /// The provider for string formatting.
        /// </param>
        /// <param name="price">
        /// The price is an instance of an object Price.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> if conversion to type <see cref = "Price"/> was successful; otherwise - false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool TryParse(string s, NumberStyles styles, IFormatProvider provider, out Price price)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("String is empty!");
            }

            decimal cost; 

            foreach (var currency1 in CurrenciesList)
            {
                var indexOf = s.IndexOf(currency1.Symbol, StringComparison.Ordinal);

                if (s.Contains(currency1.Symbol))
                {
                    cost = decimal.Parse(s.Substring(0, indexOf), styles, provider);
                    price = new Price(cost, currency1.Code);
                    return true; 
                }
            }

            foreach (var currency in Enum.GetNames(typeof(CurrencyCode)))
            {
                var indexOf = s.IndexOf(currency, StringComparison.Ordinal);

                if (s.Contains(currency))
                {
                    cost = decimal.Parse(s.Substring(0, indexOf), styles, provider);
                    price = new Price(cost, (CurrencyCode)Enum.Parse(typeof(CurrencyCode), currency));
                    return true;
                }
            }

            price = null;
            return false;
        }

        /// <summary>
        /// Determines whether the value of this instance and a specified object <see cref = "Price" />.
        /// </summary>
        /// <param name="other">
        /// The other is an instance of an object Price.
        /// </param>
        /// <returns>
        /// true <see cref="bool"/> is of type <see cref = "Price"/> and the same value as this instance; otherwise - false.
        /// </returns>
        public bool Equals(Price other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Currency != other.Currency
                        ? this.Value.Equals(Math.Round(other.Value * other.Currency.Code.GetRate(this.Currency.Code)))
                        : this.Value.Equals(other.Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">
        /// The obj is of type object.
        /// </param>
        /// <returns>
        /// true, if the value is <paramref name = "obj" /> matches the value of this instance; otherwise - false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            var price = obj as Price;
            if (price == null)
            {
                return false;
            }

            return this.Value.Equals(price.Value) && this.Currency.Equals(price.Currency);
        }

        /// <summary>
        /// Compares this instance to a specified object <see cref = "Price" /> and indicates whether this instance is before, after or at the same position in the sort order as the specified object <see cref = "Price"/>.
        /// </summary>
        /// <param name="other">
        /// The other is an instance of an object Price.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> that indicates whether this instance is before, after or at the same position in the sort order as the parameter <paramref name = "Price" />. Value Attribute Condition Less than zero This instance precedes parameter <paramref name = "Price" />. Zero This instance has the same position in the sort order as <paramref name = "Price" />. Greater than zero. This copy stands after setting <paramref name = "Price" />. -or- <paramref name = "Price" /> is null..
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// </exception>
        public int CompareTo(Price other)
        {
            if (ReferenceEquals(other, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return this.Currency != other.Currency
                       ? this.Value.CompareTo(Math.Round(other.Currency.Code.GetRate(this.Currency.Code) * other.Value))
                       : this.Value.CompareTo(other.Value);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/> hashcode.
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode() + Currency.GetHashCode();
        }

        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">
        /// object that defines the type of the returned object formatting. 
        /// </param>
        /// <returns>
        /// Instance of the object specified in the parameter <paramref name = "formatType" />, if the implementation of the <see cref = "T: System.IFormatProvider" /> can provide an object of this type; otherwise - null. 
        /// </returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null; 
        }

        public static bool operator ==(Price first, Price second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return first.Equals(second);
        }

        public static bool operator !=(Price first, Price second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return !(first == second);
        }

        public static bool operator >(Price first, Price second)
        {
            return first.CompareTo(second) > 0; 
        }

        public static bool operator >=(Price first, Price second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator <(Price first, Price second)
        {
            return first.CompareTo(second) < 0; 
        }

        public static bool operator <=(Price first, Price second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Price operator +(Price first, Price second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return first.Currency != second.Currency
                       ? new Price(first.Value + second.Currency.Code.GetRate(first.Currency.Code) * second.Value, first.Currency.Code)
                       : new Price(first.Value + second.Value, first.Currency.Code);
        }

        public static Price operator -(Price first, Price second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return first.Currency != second.Currency
                       ? new Price(first.Value - second.Currency.Code.GetRate(first.Currency.Code) * second.Value, first.Currency.Code)
                       : new Price(first.Value - second.Value, first.Currency.Code);
        }

        public static Price operator +(Price first, decimal cost)
        {
            if (ReferenceEquals(first, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return new Price(first.Value + cost, first.Currency.Code);
        }

        public static Price operator -(Price first, decimal cost)
        {
            if (ReferenceEquals(first, null))
            {
                throw new NullReferenceException("Object reference not set to an instance of the object");
            }

            return new Price(first.Value - cost, first.Currency.Code);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(this.Value.ToString("0.####") + " " + this.Currency.Code, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="formatProvider">
        /// The format provider.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                return string.Format("{0} {1}", Value.ToString("0.####", formatProvider), Currency.Code);
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            CurrencyCode currencyCode;

            decimal price;
            if (!Enum.TryParse(format, out currencyCode))
            {
                foreach (var currency in CurrenciesList.Where(currency => currency.Symbol == format))
                {
                    price = this.Currency.Code.GetRate(currency.Code) * this.Value;
                    return string.Format("{0} {1}", price.ToString("0.####", formatProvider), format);
                }
            }
            
            price = this.Currency.Code.GetRate(currencyCode) * this.Value;
            return string.Format("{0} {1}", price.ToString("0.####", formatProvider), currencyCode);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        #endregion
    }
}
