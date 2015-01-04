using System.IO;
using System.Xml.Linq;

namespace Paprotski.Lab4.InterfaceProviders
{
    public interface IXmlLinqProvider
    {
        XElement ToXElement();

        void ParseXElement(XElement xElement);
    }
}
