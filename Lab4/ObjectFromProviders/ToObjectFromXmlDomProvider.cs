using System;
using System.Text;
using System.IO;
using System.Xml;
using Paprotski.Lab4.InterfaceProviders;

namespace Paprotski.Lab4.ObjectFromProviders
{
    public static class ToObjectFromXmlDomProvider<T> where T: IXmlDomProvider, new()
    {
        public static void ToXmlWriter(IXmlDomProvider provider, Stream stream)
        {
            var settings = new XmlWriterSettings { Indent = true };
            var writer = XmlWriter.Create(stream, settings);

            provider.ToXmlNode().WriteTo(writer);
            writer.Flush();
        }

        public static T ParseXmlNode(Stream stream)
        {
            var result = new T();
            var document = new XmlDocument();
            document.Load(stream);
            result.ParseXmlNode(document);
            return result;
        }
    }
}
