namespace Paprotski.Quantity
{
    public interface IQuantity
    {
        double ConvertTo(double initiaValue, string toType, string fromType);
    }
}
