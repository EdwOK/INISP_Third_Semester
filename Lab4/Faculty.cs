using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Globalization;
using Paprotski.Lab4.InterfaceProviders;

namespace Paprotski.Lab4
{
    [Serializable]
    [DataContract(Name = "faculty")]
    [XmlRoot(ElementName = "faculty")]
    public class Faculty : IEnumerable<Group>, IEquatable<Faculty>, IXmlLinqProvider, IXmlProvider, IDisposable
    {
        #region Private Member Variables 

        [XmlIgnore, NonSerialized]
        private bool disposed = false; 

        #endregion 

        #region Ctors

        public Faculty(string title)
        {
            this.Title = title; 
            this.Groups = new ArrayList<Group>();
        }

        public Faculty(Group[] groups, string title)
        {
            this.Title = title;
            this.Groups = new ArrayList<Group>(groups);
        }

        public Faculty()
        {
            this.Groups = new ArrayList<Group>();
        }

        ~Faculty()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Public Properties 

        [XmlElement(ElementName = "title")]
        [DataMember(Name = "title", Order = 0)]
        public string Title { get; set; }

        [IgnoreDataMember]
        [XmlElement(ElementName = "count")]
        public int Count
        {
            get
            {
                return this.Groups.Count;
            }
        }
 
        [XmlArray(ElementName = "list")]
        [XmlArrayItem(ElementName = "group")]
        [DataMember(Name = "groups", Order = 1)]
        public ArrayList<Group> Groups { get; set; }

        #endregion 

        #region Public Methods 

        public XElement ToXElement()
        {
            var faculty = new XElement("faculty", 
                new XElement("title", this.Title),
                new XElement("count", this.Count));

            foreach (var group in Groups)
            {
                faculty.Add(group.ToXElement());
            }

            return faculty;
        }

        public void ParseXElement(XElement xElement)
        {
            var elementTitle = xElement.Element(XName.Get("title"));
            if (elementTitle != null)
            {
                this.Title = elementTitle.Value;
            }

            var groupsCount = 0; 
            var elementCount = xElement.Element(XName.Get("count"));
            if (elementCount != null)
            {
                groupsCount = Convert.ToInt32(elementCount.Value);
            }

            var arrayGroups = xElement.Elements(XName.Get("group")).ToArray();
            for (var i = 0; i < groupsCount; i++)
            {
                var group = new Group();
                group.ParseXElement(arrayGroups[i]);
                this.Groups.Add(group);
            }
        }

        public void ParseXmlElement(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element || reader.NodeType == XmlNodeType.Whitespace)
                {
                    continue;
                }

                if (reader.IsStartElement("title"))
                {
                    reader.ReadStartElement();
                    this.Title = reader.Value;
                }

                if (reader.IsStartElement("group"))
                {
                    var group = new Group();
                    group.ParseXmlElement(reader);
                    this.Groups.Add(group);
                }
            }
        }

        public void ToXmlElement(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("faculty");

            writer.WriteElementString("title", this.Title);
            writer.WriteElementString("count", this.Count.ToString(CultureInfo.InvariantCulture));
            writer.Flush();

            foreach (var group in Groups)
            {
                group.ToXmlElement(writer);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        /// <summary>
        /// Addition group in a faculty
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            this.Groups.Add(group);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(Faculty other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Title.Equals(other.Title);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<Group> GetEnumerator()
        {
            return this.Groups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary> 
        /// Performs application-defined tasks associated with the release or resetting unmanaged resources. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Groups.Dispose();
                }

                this.disposed = true;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("[faculty]");
            builder.AppendFormat("title={0}\ngroups={1}\n\n", this.Title, this.Groups.Count);

            foreach (var group in Groups)
            {
                builder.AppendLine(group.ToString());
            }

            return builder.ToString();
        }

        #endregion
    }
}
