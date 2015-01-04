using System;
using System.Text;
using System.IO;

namespace Paprotski.Lab3
{
    public class Student : IEquatable<Student>, IDisposable
    {
        #region Private Member Variables 

        private bool disposed = false;

        #endregion 

        #region Ctors

        public Student(string name, string surname, double averageMark, bool headman)
        {
            this.Name = name;
            this.Surname = surname;
            this.AverageMark = averageMark;
            this.Headman = headman;
        }

        public Student()
        {
            
        }

        ~Student()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion 

        #region Public Properties 

        public string Name { get; set; }

        public string Surname { get; set; }

        public double AverageMark { get; set; }

        public bool Headman { get; set; }

        #endregion 

        #region Public Methods

        /// <summary>
        /// Reading data from a stream in student object
        /// </summary>
        /// <param name="reader"></param>
        public void Load(StreamReader reader)
        {
            var sectionStudent = IniIO.ReadSection(reader, "student", 4);

            if (sectionStudent[0, 0].Contains("name") && sectionStudent[1, 0].Contains("surname")
                && sectionStudent[2, 0].Contains("averagemark") && sectionStudent[3, 0].Contains("headman"))
            {
                this.Name = sectionStudent[0, 1];
                this.Surname = sectionStudent[1, 1];

                double averageMark = 0;
                this.AverageMark = double.TryParse(sectionStudent[2, 1], out averageMark) ? averageMark : 0.0;

                bool headman = false;
                this.Headman = bool.TryParse(sectionStudent[3, 1], out headman) && headman;
            }
            else
            {
                throw new ArgumentException("Invalid input data about the student.");
            }
        }

        /// <summary>
        /// Save data to a file object student
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            var writer = new StreamWriter(stream, Encoding.Default);

            writer.WriteLine("[student]");
            writer.WriteLine(this.ToString());
            writer.Flush();
        }

        /// <summary>
        /// Reading binary data from a stream in student object
        /// </summary>
        /// <param name="stream"></param>
        public void BinaryLoad(Stream stream)
        {
            var reader = new BinaryReader(stream, Encoding.Default);

            this.Name = reader.ReadString();
            this.Surname = reader.ReadString();
            this.AverageMark = reader.ReadDouble();
            this.Headman = reader.ReadBoolean();
        }

        /// <summary>
        /// Save binary data to a file object student 
        /// </summary>
        /// <param name="stream"></param>
        public void BinarySave(Stream stream)
        {
            var writer = new BinaryWriter(stream, Encoding.Default);

            writer.Write(this.Name);
            writer.Write(this.Surname);
            writer.Write(this.AverageMark);
            writer.Write(this.Headman);
            writer.Flush();
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

            return this.Name.Equals(other.Name) && this.Surname.Equals(other.Surname)
                   && this.AverageMark.Equals(other.AverageMark) && this.Headman.Equals(other.Headman);
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
                    GC.ReRegisterForFinalize(this);
                }

                this.disposed = true;
            }
        }

        public override string ToString()
        {
            return string.Format("name={0}\nsurname={1}\naveragemark={2}\nheadman={3}\n", this.Name, this.Surname, this.AverageMark, this.Headman);
        }

        #endregion
    }
}
