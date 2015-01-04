using System;

namespace Paprotski.Quantity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class QuantityAttribute : Attribute
    {
        public string Author { get; set; }

        public string QuanityType { get; set; }
    }
}
