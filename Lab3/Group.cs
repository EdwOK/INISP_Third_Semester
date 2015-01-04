using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Paprotski.Lab3
{
    public class Group : IEnumerable<Student>, IEquatable<Group>, IDisposable
    {
        #region Private Member Variables 

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

        public ArrayList<Student> Students { get; set; }

        public int Number { get; set; }

        #endregion 

        #region Public Methods

        /// <summary>
        /// Reading data from a stream in group object
        /// </summary>
        /// <param name="reader"></param>
        public void Load(StreamReader reader)
        {
            var sectionGroup = IniIO.ReadSection(reader, "group", 2);

            var number = 0;
            this.Number = int.TryParse(sectionGroup[0, 1], out number) ? number : 0;

            var numberOfStudents = 0;
            int.TryParse(sectionGroup[1, 1], out numberOfStudents);

            for (var i = 0; i < numberOfStudents; i++)
            {
                var student = new Student();
                student.Load(reader);
                this.Students.Add(student);
            }
        }

        /// <summary>
        /// Save data to a file object group
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            var writer = new StreamWriter(stream, Encoding.Default);

            writer.WriteLine("[group]");
            writer.WriteLine("number={0}\nstudents={1}\n", this.Number, this.Students.Count);
            writer.Flush();

            foreach (var student in Students)
            {
                student.Save(writer.BaseStream);
            }
        }

        /// <summary>
        /// Reading binary data from a stream in group object
        /// </summary>
        /// <param name="stream"></param>
        public void LoadBinary(Stream stream)
        {
            var reader = new BinaryReader(stream, Encoding.Default);

            this.Number = reader.ReadInt32();
            var numberOfStudents = reader.ReadInt32();

            for (var i = 0; i < numberOfStudents; i++)
            {
                var student = new Student();
                student.BinaryLoad(reader.BaseStream);
                this.Students.Add(student);
            }
        }

        /// <summary>
        /// Save binary data to a file object group 
        /// </summary>
        /// <param name="stream"></param>
        public void SaveBinary(Stream stream)
        {
            var writer = new BinaryWriter(stream, Encoding.Default);

            writer.Write(this.Number);
            writer.Write(this.Students.Count);
            writer.Flush();

            foreach (var student in Students)
            {
                student.BinarySave(writer.BaseStream);
            }
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
        /// Average mark group 
        /// </summary>
        /// <returns>average score</returns>
        public double AverageMark()
        {
            return Students.Average(student => student.AverageMark);
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
