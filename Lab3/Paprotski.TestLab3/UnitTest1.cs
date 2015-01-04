using System;
using System.IO;
using Paprotski.Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paprotski.TestLab3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateTextFile()
        {
            var students1 = new[]
                                {
                                    new Student("Василий", "Иванов", 8.2, false), 
                                    new Student("Владимир", "Фролов", 9.6, false),
                                    new Student("Александр", "Пупкин", 10.0, true),
                                    new Student("Николай", "Андреев", 7.0, false),
                                    new Student("Антон", "Захаров", 7.5, false),
                                    new Student("Денис", "Носков", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Станислав", "Пономарёв", 7.2, false), 
                                    new Student("Егор", "Кулаков", 8.5, false),
                                    new Student("Алексей", "Калашников", 9.3, false),
                                    new Student("Николай", "Матвеев", 7.4, false),
                                    new Student("Антон", "Беляков", 10.0, true),
                                    new Student("Георгий", "Фомичёв", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");
            var fileStream = File.Open("D:/test.txt", FileMode.OpenOrCreate);
            faculty.Save(fileStream);
        }

        [TestMethod]
        public void TestReadTextFile()
        {
            var students = new[]
                                {
                                    new Student("Станислав", "Пономарёв", 7.2, false), 
                                    new Student("Егор", "Кулаков", 8.5, false),
                                    new Student("Алексей", "Калашников", 9.3, false),
                                    new Student("Николай", "Матвеев", 7.4, false),
                                    new Student("Антон", "Беляков", 10.0, true),
                                    new Student("Георгий", "Фомичёв", 7.9, false)
                                };

            var group = new Group(students, 353503);
            var fileStream = File.Open("D:/test.txt", FileMode.OpenOrCreate);
            var faculty = new Faculty(); 
            faculty.Load(fileStream);

            Assert.AreEqual(faculty.Title, "fksis");
            Assert.AreEqual(faculty.Groups.Contains(group), true);
        }

        [TestMethod]
        public void TestCreateBinaryFile()
        {
            var students1 = new[]
                                {
                                    new Student("Василий", "Иванов", 8.2, false), 
                                    new Student("Владимир", "Фролов", 9.6, false),
                                    new Student("Александр", "Пупкин", 10.0, true),
                                    new Student("Николай", "Андреев", 7.0, false),
                                    new Student("Антон", "Захаров", 7.5, false),
                                    new Student("Денис", "Носков", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Станислав", "Пономарёв", 7.2, false), 
                                    new Student("Егор", "Кулаков", 8.5, false),
                                    new Student("Алексей", "Калашников", 9.3, false),
                                    new Student("Николай", "Матвеев", 7.4, false),
                                    new Student("Антон", "Беляков", 10.0, true),
                                    new Student("Георгий", "Фомичёв", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");
            var fileStream = File.Open("D:/test.dat", FileMode.OpenOrCreate); 
            faculty.BinarySave(fileStream);
        }


        [TestMethod]
        public void TestReadBinaryFile()
        {
            var students = new[]
                                {
                                    new Student("Василий", "Иванов", 8.2, false), 
                                    new Student("Владимир", "Фролов", 9.6, false),
                                    new Student("Александр", "Пупкин", 10.0, true),
                                    new Student("Николай", "Андреев", 7.0, false),
                                    new Student("Антон", "Захаров", 7.5, false),
                                    new Student("Денис", "Носков", 8.9, false)
                                };

            var group = new Group(students, 353501);
            var fileStream = File.Open("D:/test.dat", FileMode.OpenOrCreate);
            var faculty = new Faculty();
            faculty.BinaryLoad(fileStream);

            Assert.AreEqual(faculty.Title, "fksis");
            Assert.AreEqual(faculty.Groups.Contains(group), true);
        }

        [TestMethod]
        public void TestCreateDeflateFile()
        {
            var students1 = new[]
                                {
                                    new Student("Василий", "Иванов", 8.2, false), 
                                    new Student("Владимир", "Фролов", 9.6, false),
                                    new Student("Александр", "Пупкин", 10.0, true),
                                    new Student("Николай", "Андреев", 7.0, false),
                                    new Student("Антон", "Захаров", 7.5, false),
                                    new Student("Денис", "Носков", 8.9, false)
                                };

            var students2 = new[]
                                {
                                    new Student("Станислав", "Пономарёв", 7.2, false), 
                                    new Student("Егор", "Кулаков", 8.5, false),
                                    new Student("Алексей", "Калашников", 9.3, false),
                                    new Student("Николай", "Матвеев", 7.4, false),
                                    new Student("Антон", "Беляков", 10.0, true),
                                    new Student("Георгий", "Фомичёв", 7.9, false)
                                };

            var groups = new[] { new Group(students1, 353501), new Group(students2, 353503) };
            var faculty = new Faculty(groups, "fksis");
            var fileStream = File.Open("D:/test.bin", FileMode.OpenOrCreate);
            faculty.DeflateSave(fileStream);
        }

        [TestMethod]
        public void TestReadDeflateFile()
        {
            var students = new[]
                                {
                                    new Student("Василий", "Иванов", 8.2, false), 
                                    new Student("Владимир", "Фролов", 9.6, false),
                                    new Student("Александр", "Пупкин", 10.0, true),
                                    new Student("Николай", "Андреев", 7.0, false),
                                    new Student("Антон", "Захаров", 7.5, false),
                                    new Student("Денис", "Носков", 8.9, false)
                                };

            var group = new Group(students, 353501);

            var fileStream = File.Open("D:/test.bin", FileMode.OpenOrCreate);
            var faculty = new Faculty();
            faculty.DeflateLoad(fileStream);

            Assert.AreEqual(faculty.Title, "fksis");
            Assert.AreEqual(faculty.Groups.Contains(group), true);
        }
    }
}
