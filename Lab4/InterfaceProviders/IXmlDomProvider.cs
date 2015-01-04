using System.Xml;

namespace Paprotski.Lab4.InterfaceProviders
{
    public interface IXmlDomProvider
    {
        XmlNode ToXmlNode();

        void ParseXmlNode(XmlNode xmlNode);
    }
}
