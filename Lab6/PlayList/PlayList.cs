using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Paprotski.Lab6
{
    [DataContract(Name = "playlist")]
    public class PlayList
    {
        #region Fields

        private TimeSpan playListTime;

        private double rating;

        #endregion

        #region Ctors

        public PlayList(int id, string title, IEnumerable<Song> songs = null)
        {
            this.Id = id;
            this.Title = title;
            this.SongList = songs != null ? new SortedList<int, Song>(songs.ToDictionary(song => song.Id)) : new SortedList<int, Song>();
        }

        public PlayList() : this(0, "PlayList", new [] { new Song()})
        {
        }

        #endregion

        #region Public Properties

        [DataMember(Order = 0)]
        public int Id { get; private set; }

        [DataMember(Order = 1)]
        public string Title { get; set; }

        [DataMember(Order = 2)]
        public TimeSpan PlayListTime
        {
            get
            {
                if (this.SongList.Count == 0)
                {
                    return TimeSpan.Zero;
                }

                this.playListTime = this.SongList.Aggregate(TimeSpan.Zero, (current, song) => current + song.Value.TrackTime);
                return this.playListTime;
            }
            set
            {
                this.playListTime = value;
            }
        }

        [DataMember(Order = 3)]
        public double Rating
        {
            get
            {
                if (this.SongList.Count == 0)
                {
                    return default(double);
                }

                this.rating = this.SongList.Average(song => song.Value.Rating);
                return this.rating;
            }
            set
            {
                this.rating = value;
            }
        }

        [DataMember(Order = 4)]
        public SortedList<int, Song> SongList { get; set; }

        #endregion

        #region Public Methods and Operators

        public static PlayList DeserializeJson(string pathList)
        {
            if (string.IsNullOrWhiteSpace(pathList))
            {
                return null;
            }

            if (!File.Exists(pathList))
            {
                return null;
            }

            var serializer = new DataContractJsonSerializer(typeof(PlayList));
            using (var fileStream = File.OpenRead(pathList))
            {
                return (PlayList)serializer.ReadObject(fileStream);
            }
        }

        public static List<PlayList> LoadPlayLists(string pathLists)
        {
            try
            {
                var files = Directory.GetFiles(pathLists, "*.json");
                return files.Select(DeserializeJson).ToList();
            }
            catch
            {
                return null;
            }
        }

        public void SerializeJson(string pathList)
        {
            if (string.IsNullOrWhiteSpace(pathList))
            {
                return;
            }

            if (!Directory.Exists(pathList))
            {
                Directory.CreateDirectory(pathList);
            }

            var serializer = new DataContractJsonSerializer(typeof(PlayList));
            using (var fileStream = File.Create(pathList + this.Title + ".json"))
            {
                serializer.WriteObject(fileStream, this);
            }
        }

        public override sealed int GetHashCode()
        {
            return this.Title.GetHashCode() ^ this.PlayListTime.GetHashCode() ^ this.Rating.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("ID: {0} Playlist: {1}, Rating: {2}\n", this.Id, this.Title, this.Rating);

            foreach (var song in this.SongList)
            {
                builder.AppendLine(song.Value.ToString());
            }

            return builder.ToString();
        }

        #endregion
    }
}