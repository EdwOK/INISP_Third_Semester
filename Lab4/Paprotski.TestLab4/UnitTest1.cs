using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using Paprotski.Lab4;
using Paprotski.Lab4.ObjectFromProviders;

namespace Paprotski.TestLab4
{
    using System.Text;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRuntimeSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            var formatter = new BinaryFormatter();

            using (var stream = File.Create("D:/faculty_runtime.dat"))
            {
                formatter.Serialize(stream, faculty);
            }

            var newFaculty = new Faculty();

            using (var stream = File.OpenRead("D:/faculty_runtime.dat"))
            {
                newFaculty = (Faculty)formatter.Deserialize(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Count, 2);
        }

        [TestMethod]
        public void TestDataContractSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            var dataContract = new DataContractSerializer(typeof(Faculty));
            using (var stream = File.Create("D:/faculty_contract.xml"))
            {
                dataContract.WriteObject(stream, faculty);
            }

            var newFaculty = new Faculty();

            using (var stream = File.OpenRead("D:/faculty_contract.xml"))
            {
                newFaculty = (Faculty)dataContract.ReadObject(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestNetDataContractSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            var netDataContract = new NetDataContractSerializer();
            using (var stream = File.Create("D:/faculty_net_contract.xml"))
            {
                netDataContract.Serialize(stream, faculty);
            }

            Faculty newFaculty;

            using (var stream = File.OpenRead("D:/faculty_net_contract.xml"))
            {
                newFaculty = (Faculty)netDataContract.Deserialize(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestDataContractJsonSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            var jsonSerializer = new DataContractJsonSerializer(typeof(Faculty));
            using (var stream = File.Create("D:/faculty_json.json"))
            {
                jsonSerializer.WriteObject(stream, faculty);
            }

            Faculty newFaculty;

            using (var stream = File.OpenRead("D:/faculty_json.json"))
            {
                newFaculty = (Faculty)jsonSerializer.ReadObject(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestXmlSerialisation()
        {
            var student = new Student("Vasiliy", "Ivanov", 8.2, false);

            var xmlSerializer = new XmlSerializer(typeof(Student));
            using (var stream = File.Create("D:/student.xml"))
            {
                xmlSerializer.Serialize(stream, student);
            }

            Student newStudent;

            using (var stream = File.OpenRead("D:/student.xml"))
            {
                newStudent = (Student)xmlSerializer.Deserialize(stream);
            }

            Assert.AreEqual(newStudent.Name, student.Name);
            Assert.AreEqual(newStudent.Surname, student.Surname);
            Assert.AreEqual(newStudent.Gpa, student.Gpa);
            Assert.AreEqual(newStudent.Headman, student.Headman);
        }

        [TestMethod]
        public void TestXmlLinqSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            using (var stream = File.Create("D:/faculty_linq.xml"))
            {
                ToObjectFromXmlLinqProvider<Faculty>.ToXmlWriter(faculty, stream);
            }

            Faculty newFaculty;

            using (var stream = File.OpenRead("D:/faculty_linq.xml"))
            {
                newFaculty = ToObjectFromXmlLinqProvider<Faculty>.ParseXElement(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestXmlWriteAndReadSerialisation()
        {
            var students1 = new[]
                                {
                                    new Student("Vasiliy", "Ivanov", 8.2, false), 
                                    new Student("Vladimir", "Frolov", 9.6, false),
                                    new Student("Alex", "Pupkin", 10.0, true),
                                    new Student("Nikolay", "Andreev", 7.0, false),
                                    new Student("Anton", "Zaxarchenko", 7.5, false),
                                    new Student("Denis", "Noskov", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Stanislav", "Ponomariev", 7.2, false), 
                                    new Student("Egor", "Kylakov", 8.5, false),
                                    new Student("Alex", "Kalashnikov", 9.3, false),
                                    new Student("Nikolay", "Matveev", 7.4, false),
                                    new Student("Anton", "Belyakov", 10.0, true),
                                    new Student("Gena", "Fomichev", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");

            using (var stream = File.Create("D:/faculty_io.xml"))
            {
                ToObjectFromXmlProvider<Faculty>.ToXmlWriter(faculty, stream);
            }

            Faculty newFaculty;

            using (var stream = File.OpenRead("D:/faculty_io.xml"))
            {
                newFaculty = ToObjectFromXmlProvider<Faculty>.ParseXmlElement(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestWriteLinqReadXmlMethods()
        {
            Faculty newFaculty; 

            using (var stream = File.OpenRead("D:/faculty_linq.xml"))
            {
                newFaculty = ToObjectFromXmlProvider<Faculty>.ParseXmlElement(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }

        [TestMethod]
        public void TestWriteXmlReadLinqMethods()
        {
            Faculty newFaculty;

            using (var stream = File.OpenRead("D:/faculty_io.xml"))
            {
                newFaculty = ToObjectFromXmlLinqProvider<Faculty>.ParseXElement(stream);
            }

            Assert.AreEqual(newFaculty.Title, "fksis");
            Assert.AreEqual(newFaculty.Groups.Count, 2);
            Assert.AreEqual(newFaculty.Groups[0].Count, 6);
        }
    }
}
