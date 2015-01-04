using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Globalization;
using Paprotski.Lab4.InterfaceProviders;

namespace Paprotski.Lab4
{
    using System.Collections;
    using System.Collections.Generic;

    [Serializable]
    [DataContract(Name = "student")]
    [XmlRoot(ElementName = "student")]
    public class Student : IEquatable<Student>, IXmlLinqProvider, IXmlDomProvider, IXmlProvider, IDisposable
    {
        #region Private Member Variables 

        [XmlIgnore, NonSerialized]
        private bool disposed = false;

        #endregion 

        #region Ctors

        /// <summary>
        /// Ctor of student 
        /// </summary>
        /// <param name="gpa">grade point average</param>
        public Student(string name, string surname, double gpa, bool headman)
        {
            this.Name = name;
            this.Surname = surname;
            this.Gpa = gpa;
            this.Headman = headman;
        }

        public Student()
        {
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Student()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion 

        #region Public Properties 

        [XmlAttribute(AttributeName = "name")]
        [DataMember(Name = "name", Order = 0)]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "surname")]
        [DataMember(Name = "surname", Order = 1)]
        public string Surname { get; set; }

        [XmlAttribute(AttributeName = "gpa")]
        [DataMember(Name = "gpa", Order = 2)]
        public double Gpa { get; set; }

        [XmlAttribute(AttributeName = "headman")]
        [DataMember(Name = "headman", Order = 3)]
        public bool Headman { get; set; }

        #endregion 

        #region Public Methods

        public XElement ToXElement()
        {
            var student = new XElement("student",
                new XElement("name", this.Name),
                new XElement("surname", this.Surname),
                new XElement("gpa", this.Gpa.ToString(CultureInfo.InvariantCulture)),
                new XElement("headman", this.Headman));

            return student;
        }

        public void ParseXElement(XElement xElement)
        {
            var arrayStudent = xElement.Elements().ToArray();
            
            this.Name = arrayStudent[0].Value;
            this.Surname = arrayStudent[1].Value;
            this.Gpa = Convert.ToDouble(arrayStudent[2].Value, CultureInfo.InvariantCulture);
            this.Headman = Convert.ToBoolean(arrayStudent[3].Value);
        }

        public void ToXmlElement(XmlWriter writer)
        {
            writer.WriteStartElement("student");

            writer.WriteElementString("name", this.Name);
            writer.WriteElementString("surname", this.Surname);
            writer.WriteElementString("gpa", this.Gpa.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("headman", this.Headman.ToString());

            writer.WriteEndElement();
            writer.Flush();
        }

        public void ParseXmlElement(XmlReader reader)
        {
            if (reader.IsStartElement("student"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "student")
                    {
                        break;
                    }

                    if (reader.NodeType != XmlNodeType.Element || reader.NodeType == XmlNodeType.Whitespace)
                    {
                        continue;
                    }

                    if (reader.IsStartElement("name"))
                    {
                        reader.ReadStartElement();
                        this.Name = reader.Value;
                    }

                    if (reader.IsStartElement("surname"))
                    {
                        reader.ReadStartElement();
                        this.Surname = reader.Value;
                    }

                    if (reader.IsStartElement("gpa"))
                    {
                        reader.ReadStartElement();
                        this.Gpa = Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture);
                    }

                    if (reader.IsStartElement("headman"))
                    {
                        reader.ReadStartElement();
                        this.Headman = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
                    }
                }
            }
        }

        public XmlNode ToXmlNode()
        {
            var document = new XmlDocument();
            var root = document.CreateElement("student");

            var name = document.CreateElement("name");
            name.InnerText = this.Name;
            var surname = document.CreateElement("surname");
            surname.InnerText = this.Surname;
            var gpa = document.CreateElement("gpa");
            gpa.InnerText = this.Gpa.ToString(CultureInfo.InvariantCulture);
            var headman = document.CreateElement("headman");
            headman.InnerText = this.Headman.ToString(CultureInfo.InvariantCulture);

            root.AppendChild(name);
            root.AppendChild(surname);
            root.AppendChild(gpa);
            root.AppendChild(headman); 
           
            document.AppendChild(root);
            return document;
        }

        public void ParseXmlNode(XmlNode xmlNode)
        {
            var nodes = xmlNode.ChildNodes;
            var student = nodes.Cast<XmlNode>().First(node => node.Name = "student").ChildNodes;
            this.Name = student[0].InnerText;
            this.Surname = student[1].InnerText;
            this.Gpa = Convert.ToDouble(student[2].InnerText, CultureInfo.InvariantCulture);
            this.Headman = Convert.ToBoolean(student[3].InnerText); 
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
        public bool Equals(Student other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, null))
            {
                return false; 
            }

            return this.Name.Equals(other.Name) && this.Surname.Equals(other.Surname)
                   && this.Gpa.Equals(other.Gpa) && this.Headman.Equals(other.Headman);
        }

        /// <summary>
        /// Equales obj to student 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Student)obj); 
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
                this.disposed = true;
            }
        }

        /// <summary>
        /// Hash code to object 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.disposed.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Surname != null ? this.Surname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Gpa.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Headman.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Object to string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("name={0}\nsurname={1}\ngpa={2}\nheadman={3}\n", this.Name, this.Surname, this.Gpa, this.Headman);
        }

        #endregion
    }
}
