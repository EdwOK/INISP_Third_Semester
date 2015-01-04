using System.Xml;
using System.IO;
using Paprotski.Lab4.InterfaceProviders;
using System.Text;

namespace Paprotski.Lab4.ObjectFromProviders
{
    public static class ToObjectFromXmlProvider<T> where T: IXmlProvider, new()
    {
        public static void ToXmlWriter(IXmlProvider provider, Stream stream)
        {
            var settings = new XmlWriterSettings { Indent = true };
            var writer = XmlWriter.Create(stream, settings);
            provider.ToXmlElement(writer); 
        }

        public static T ParseXmlElement(Stream stream)
        {
            var result = new T();
            var settings = new XmlReaderSettings { IgnoreComments = true };
            var reader = XmlReader.Create(stream, settings);
            result.ParseXmlElement(reader);
            return result; 
        }
    }
}
