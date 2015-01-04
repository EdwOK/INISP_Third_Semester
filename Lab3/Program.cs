using System;
using System.IO;
using System.Security.Permissions;

namespace Paprotski.Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var watcher = new FileSystemWatcher
            {
                Path = @"D:\",
                Filter = "*.txt",
                IncludeSubdirectories = true
            };

            watcher.Created += ChangeHandler;
            watcher.Changed += ChangeHandler;
            watcher.Deleted += ChangeHandler;
            watcher.Renamed += RenamedHandler;
            watcher.Error += ErrorHander; 

            watcher.EnableRaisingEvents = true;

            var fileStream = File.Open("D:/test.txt", FileMode.OpenOrCreate);
            var faculty = new Faculty();
            faculty.Load(fileStream);

            Console.WriteLine(faculty);
            watcher.Dispose();
            Console.ReadKey();
        }

        static void ChangeHandler(object obj, FileSystemEventArgs eventArgs)
        {
            Console.WriteLine("File {0} was {1}", eventArgs.FullPath, eventArgs.ChangeType);
        }

        static void RenamedHandler(object obj, RenamedEventArgs eventArgs)
        {
            Console.WriteLine("Renamed: {0} -> {1}", eventArgs.OldFullPath, eventArgs.FullPath);
        }

        static void ErrorHander(object obj, ErrorEventArgs eventArgs)
        {
            Console.WriteLine("Error: {0}", eventArgs.GetException().Message);
        }
    }
}
