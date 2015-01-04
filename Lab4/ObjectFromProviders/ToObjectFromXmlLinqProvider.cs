using System.IO;
using System.Text;
using System.Xml.Linq;
using Paprotski.Lab4.InterfaceProviders;

namespace Paprotski.Lab4.ObjectFromProviders
{
    public static class ToObjectFromXmlLinqProvider<T> where T: IXmlLinqProvider, new()
    {
        public static void ToXmlWriter(IXmlLinqProvider provider, Stream stream)
        {
            var writer = new StreamWriter(stream);
            writer.WriteLine(new XDeclaration("1.0", "utf-8", "yes"));
            writer.WriteLine(provider.ToXElement());
            writer.Flush();
        }

        public static T ParseXElement(Stream stream)
        {
            var result = new T();
            var reader = new StreamReader(stream);
            reader.ReadLine();
            result.ParseXElement(XElement.Parse(reader.ReadToEnd()));
            return result; 
        }
    }
}
