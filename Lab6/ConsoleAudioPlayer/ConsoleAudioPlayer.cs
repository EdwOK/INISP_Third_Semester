using System;
using System.Collections.Generic;

namespace Paprotski.Lab6.ConsoleAudioPlayer
{
    using System.IO;
    using System.Runtime.Remoting.Messaging;

    using Paprotski.Lab6.AudioPlayer;

    public static class ConsoleAudioPlayer
    {
        private static AudioPlayer audioPlayer;

        private static ControllerType controllerType = ControllerType.All;

        private static bool hotkeycontrol = false;

        public static void Main(string[] args)
        {
            var songs1 = new[]
                           {
                               new Song(0, "Die, Die My Darling", "Metallica", "0:15", "Rock", 9), 
                               new Song(1, "Back In Black", "AC/DC", "0:05", "Rock", 8), 
                               new Song(2, "Wind of Change", "Scorpions", "0:09", "Rock", 7), 
                               new Song(3, "Burn It Down", "Linkin Park", "0:10", "Rock", 10), 
                               new Song(4, "Dumb", "Nirvana", "0:20", "Rock", 6)
                           };

            var songs2 = new[]
                           {
                               new Song(0, "Imagine", "John Lennon", "0:05", "Pop", 6), 
                               new Song(1, "We Are The Champions", "Queen", "0:05", "Pop", 10), 
                               new Song(2, "Sacrifice", "Elton John", "0:05", "Pop", 9), 
                               new Song(3, "Route 66", "The Rolling Stones", "0:05", "Pop", 10), 
                               new Song(4, "Happy Birthday", "The Beatles", "0:05", "Pop/Rock", 8)
                           };

            var playlist1 = new PlayList(0, "Rock Music", songs1);
            playlist1.SerializeJson("D:/test/");
            var playlist2 = new PlayList(1, "Pop Music", songs2);
            playlist2.SerializeJson("D:/test/");

            audioPlayer = new AudioPlayer(new List<PlayList> { playlist1, playlist2 });
            audioPlayer.StateChanged += OnAudioPlayerStateChanged;
            //ConsoleUpdate();
            //Menu();

            for (var i = 0; i < 1000; ++i)
            {
                cache[i] = -1;
            }

            Console.WriteLine(Result(27));
            Console.ReadKey();
        }

        private static long[] cache = new long[1000 + 1];

        private static long Result(int number)
        {
            if (number < 0)
            {
                return 0;
            }
            if (number == 0)
            {
                return 1;
            }

            if (cache[number] < 0)
            {
                cache[number] = Result(number - 3) + Result(number - 4) + Result(number - 7);
                cache[number] %= 1000000007;
            }

            return cache[number];
        }

        private static void Menu()
        {
            while (true)
            {
                ConsoleUpdate();
                var input = Console.ReadLine();

                if (input == "cn")
                {
                    hotkeycontrol = true;
                    break; 
                }

                try
                {
                    var commandsLine = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var commandType = commandsLine[commandsLine.Length - 2];
                    var playListId = int.Parse(commandsLine[commandsLine.Length - 1]);

                    switch (commandType)
                    {
                        case "play":
                        case "pl":
                            audioPlayer.Execute(playListId, CommandType.Play);
                            break;
                        case "pause":
                        case "pa":
                            audioPlayer.Execute(playListId, CommandType.Pause);
                            break;
                        case "resume":
                        case "rs":
                            audioPlayer.Execute(playListId, CommandType.Resume);
                            break;
                        case "stop":
                        case "st":
                            audioPlayer.Execute(playListId, CommandType.Stop);
                            break;
                        case "exit":
                        case "ex":
                            audioPlayer.Dispose();
                            return;
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            ControlMenu();
        }

        private static void ControlMenu()
        {
            ConsoleUpdate();
            var consoleKey = Console.ReadKey(); 

            while (consoleKey.Key != ConsoleKey.DownArrow)
            {
                switch (consoleKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        controllerType = ControllerType.All;
                        break;
                    case ConsoleKey.RightArrow:
                        controllerType = ControllerType.Active;
                        break;
                    case ConsoleKey.UpArrow:
                        controllerType = ControllerType.Passive;
                        break;
                }

                ConsoleUpdate();
                consoleKey = Console.ReadKey();
            }

            hotkeycontrol = false; 
            Menu();
        }

        private static void OnAudioPlayerStateChanged(object sender, EventArgs eventArgs)
        {
            ConsoleUpdate(); 
        }

        private static void ConsoleUpdate()
        {
            var inputConsole = audioPlayer.GetState(controllerType);
            var line = new string('~', 50);

            Console.Clear();
            Console.WriteLine(line);
            Console.WriteLine(inputConsole.Item1);
            Console.WriteLine(line);
            Console.WriteLine(inputConsole.Item2);

            if (hotkeycontrol)
            {
                Console.WriteLine("Help: ← - all, → - active, ↑ - passive, ↓ - exit");
                Console.Write("Key: ");
            }
            else
            {
                Console.WriteLine("Help: play(pl), stop(st), pause(pa), resume(rs), exit(ex) and number playlist");
                Console.Write("Command: ");
            }
        }
    }
}
