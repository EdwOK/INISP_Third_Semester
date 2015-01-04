using System;
using System.IO;
using Paprotski.Lab4.ObjectFromProviders;

namespace Paprotski.Lab4
{
    using System.Runtime.Serialization;

    class Program
    {
        static void Main(string[] args)
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

            var student = new Student("Vasiliy", "Ivanov", 8.2, false);

            using (var steam = File.Create("D:/test.xml"))
            {
                ToObjectFromXmlDomProvider<Student>.ToXmlWriter(student, steam);
            }

            Student newStudent;

            using (var steam = File.OpenRead("D:/test.xml"))
            {
                newStudent = (Student)ToObjectFromXmlDomProvider<Student>.ParseXmlNode(steam);
            }
            Console.ReadKey();
        }
    }
}
