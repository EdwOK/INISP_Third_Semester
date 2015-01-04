using System.Xml;

namespace Paprotski.Lab4.InterfaceProviders
{
    public interface IXmlProvider
    {
        void ParseXmlElement(XmlReader reader);

        void ToXmlElement(XmlWriter writer);
    }
}
