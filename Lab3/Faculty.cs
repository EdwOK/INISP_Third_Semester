using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Paprotski.Lab3
{
    using System.Security.Cryptography;

    public class Faculty : IEnumerable<Group>, IEquatable<Faculty>, IDisposable
    {
        #region Private Member Variables 

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

        public ArrayList<Group> Groups { get; set; }

        public string Title { get; set; }

        #endregion 

        #region Public Methods 

        /// <summary>
        /// Reading data from a stream in faculty object
        /// </summary>
        /// <param name="stream"></param>
        public void Load(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.Default))
            {
                var facultyInfo = IniIO.ReadSection(reader, "faculty", 2);

                this.Title = facultyInfo[0, 1];
                var numberOfGroups = 0;
                int.TryParse(facultyInfo[1, 1], out numberOfGroups);

                for (var i = 0; i < numberOfGroups; i++)
                {
                    var group = new Group();
                    group.Load(reader);
                    this.Groups.Add(group);
                }
            }
        }

        /// <summary>
        /// Save data to a file object faculty 
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding.Default))
            {
                writer.WriteLine("[faculty]");
                writer.WriteLine("title={0}\ngroups={1}\n", this.Title, this.Groups.Count);
                writer.Flush();

                foreach (var group in Groups)
                {
                    group.Save(writer.BaseStream);
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Reading binary data from a stream in faculty object
        /// </summary>
        /// <param name="stream"></param>
        public void BinaryLoad(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default))
            {
                this.Title = reader.ReadString();
                var numberOfGroups = reader.ReadInt32();

                for (var i = 0; i < numberOfGroups; i++)
                {
                    var group = new Group();
                    group.LoadBinary(reader.BaseStream);
                    this.Groups.Add(group);
                }
            }
        }

        /// <summary>
        /// Save binary data to a file object faculty 
        /// </summary>
        /// <param name="stream"></param>
        public void BinarySave(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default))
            {
                writer.Write(this.Title);
                writer.Write(this.Groups.Count);
                writer.Flush();

                foreach (var group in Groups)
                {
                    group.SaveBinary(writer.BaseStream);
                }
            }
        }

        /// <summary>
        /// Reading data from a stream in faculty object 
        /// </summary>
        /// <param name="stream"></param>
        public void DeflateLoad(Stream stream)
        {
            using (var deflate = new DeflateStream(stream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(deflate, Encoding.Default))
                {
                    var facultyInfo = IniIO.ReadSection(reader, "faculty", 2);

                    this.Title = facultyInfo[0, 1];
                    var numberOfGroups = 0;
                    int.TryParse(facultyInfo[1, 1], out numberOfGroups);

                    for (var i = 0; i < numberOfGroups; i++)
                    {
                        var group = new Group();
                        group.Load(reader);
                        this.Groups.Add(group);
                    }
                }
            }
        }

        /// <summary>
        /// Save data to a deflate file object faculty 
        /// </summary>
        /// <param name="stream"></param>
        public void DeflateSave(Stream stream)
        {
            using (var deflate = new DeflateStream(stream, CompressionMode.Compress))
            {
                using (var writer = new StreamWriter(deflate, Encoding.Default))
                {
                    writer.WriteLine("[faculty]");
                    writer.WriteLine("title={0}\ngroups={1}\n", this.Title, this.Groups.Count);
                    writer.Flush();

                    foreach (var group in Groups)
                    {
                        group.Save(writer.BaseStream);
                        writer.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Reading data from a stream in faculty object
        /// </summary>
        /// <param name="stream"></param>
        public void LoadEncryptorFile(Stream stream)
        {
            using (var rijAlg = Rijndael.Create())
            {
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (var crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(crypto, Encoding.Default))
                    {
                        var facultyInfo = IniIO.ReadSection(reader, "faculty", 2);

                        this.Title = facultyInfo[0, 1];
                        var numberOfGroups = 0;
                        int.TryParse(facultyInfo[1, 1], out numberOfGroups);

                        for (var i = 0; i < numberOfGroups; i++)
                        {
                            var group = new Group();
                            group.Load(reader);
                            this.Groups.Add(group);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save data to a file object faculty 
        /// </summary>
        /// <param name="stream"></param>
        public void SaveDecryptorFile(Stream stream)
        {
            using (var rijAlg = Rijndael.Create())
            {
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(crypto, Encoding.Default))
                    {
                        writer.WriteLine("[faculty]");
                        writer.WriteLine("title={0}\ngroups={1}\n", this.Title, this.Groups.Count);
                        writer.Flush();

                        foreach (var group in Groups)
                        {
                            group.Save(writer.BaseStream);
                            writer.WriteLine();
                        }
                    }
                }
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
