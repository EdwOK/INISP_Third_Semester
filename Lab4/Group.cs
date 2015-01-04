using System;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Paprotski.Lab4.InterfaceProviders;

namespace Paprotski.Lab4
{
    using Paprotski.Lab4.ObjectFromProviders;

    [Serializable]
    [DataContract(Name = "group")]
    [XmlRoot(ElementName = "group")]
    public class Group : IEnumerable<Student>, IEquatable<Group>, IXmlLinqProvider, IXmlProvider, IDisposable
    {
        #region Private Member Variables 

        [XmlIgnore, NonSerialized]
        private bool disposed = false;

        #endregion 

        #region Ctors

        public Group(int number)
        {
            this.Number = number; 
            this.Students = new ArrayList<Student>();
        }

        public Group(Student[] students, int number)
        {
            this.Number = number; 
            this.Students = new ArrayList<Student>(students);
        }

        public Group()
        {
            this.Students = new ArrayList<Student>();
        }

        ~Group()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion 

        #region Public Properties 

        [XmlElement(ElementName = "number")]
        [DataMember(Name = "number", Order = 0)]
        public int Number { get; set; }


        [IgnoreDataMember]
        [XmlElement(ElementName = "count")]
        public int Count
        {
            get
            {
                return this.Students.Count;
            }
        }

        [XmlArray(ElementName = "list")]
        [XmlArrayItem(ElementName = "student")]
        [DataMember(Name = "students", Order = 1)]
        public ArrayList<Student> Students { get; set; }

        #endregion 

        #region Public Methods

        public XElement ToXElement()
        {
            var group = new XElement("group", 
                new XElement("number", this.Number),
                new XElement("count", this.Count));

            foreach (var student in Students)
            {
                group.Add(student.ToXElement());
            }

            return group;
        }

        public void ParseXElement(XElement xElement)
        {
            var elementNumber = xElement.Element(XName.Get("number"));
            if (elementNumber != null)
            {
                this.Number = Convert.ToInt32(elementNumber.Value);
            }

            var studentsCount = 0;
            var elementCount = xElement.Element(XName.Get("count"));
            if (elementCount != null)
            {
                studentsCount = Convert.ToInt32(elementCount.Value);
            }

            var studentsElement = xElement.Elements(XName.Get("student")).ToArray();
            for (var i = 0; i < studentsCount; i++)
            {
                var student = new Student();
                student.ParseXElement(studentsElement[i]);
                this.Students.Add(student);
            }
        }

        public void ToXmlElement(XmlWriter writer)
        {
            writer.WriteStartElement("group");

            writer.WriteElementString("number", this.Number.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("count", this.Count.ToString(CultureInfo.InvariantCulture));
            writer.Flush();

            foreach (var student in Students)
            {
                student.ToXmlElement(writer);
            }

            writer.WriteEndElement();
            writer.Flush();
        }

        public void ParseXmlElement(XmlReader reader)
        {
            if (reader.IsStartElement("group"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "group")
                    {
                        break;
                    }

                    if (reader.NodeType != XmlNodeType.Element || reader.NodeType == XmlNodeType.Whitespace)
                    {
                        continue;
                    }

                    if (reader.IsStartElement("number"))
                    {
                        reader.ReadStartElement();
                        this.Number = Convert.ToInt32(reader.Value);
                    }

                    if (reader.IsStartElement("student"))
                    {
                        var student = new Student();
                        student.ParseXmlElement(reader);
                        this.Students.Add(student);
                    }
                }
            }
        }

        /// <summary>
        /// Addition student in a group
        /// </summary>
        /// <param name="student"></param>
        public void Add(Student student)
        {
            this.Students.Add(student);
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
        public bool Equals(Group other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Number.Equals(other.Number);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<Student> GetEnumerator()
        {
            return this.Students.GetEnumerator();
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
                    this.Students.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Grade Point Average group
        /// </summary>
        /// <returns>average score</returns>
        public double CalculateGroupGpa()
        {
            return Students.Average(student => student.Gpa);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("[group]");
            builder.AppendFormat("number={0}\nstudents={1}\n\n", this.Number, this.Students.Count);

            foreach (var student in Students)
            {
                builder.AppendLine(student.ToString());
            }

            return builder.ToString();
        }

        #endregion
    }
}
