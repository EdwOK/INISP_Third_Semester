using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Paprotski.Lab6
{
    [DataContract(Name = "song")]
    public class Song : IComparable<Song>, IEquatable<Song>
    {
        #region Ctors

        public Song(int id, string title, string artist, string length, string genre, int rating)
        {
            this.Id = id;
            this.Title = title;
            this.Artist = artist;
            this.TrackTime = TimeSpan.ParseExact(length, @"m\:ss", CultureInfo.CurrentCulture);
            this.Genre = genre;
            this.Rating = rating;
        }

        public Song()
            : this(0, "Song", "Artist", "0:0:0", "Gengre", 0)
        {
        }

        #endregion

        #region Public Properties

        [DataMember(Order = 2)]
        public string Artist { get; set; }

        [DataMember(Order = 4)]
        public string Genre { get; set; }

        [DataMember(Order = 0)]
        public int Id { get; private set; }

        [DataMember(Order = 5)]
        public double Rating { get; set; }

        [DataMember(Order = 1)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public TimeSpan TrackTime { get; set; }

        #endregion

        #region Public Methods and Operators

        public static bool operator ==(Song first, Song second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return Equals(first, second);
            }

            return first.Equals(second);
        }

        public static bool operator !=(Song first, Song second)
        {
            return !(first == second);
        }

        public int CompareTo(Song other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            var song = (Song)obj;

            return this.Id == song.Id && this.Title == song.Title && this.Artist == song.Artist
                   && this.TrackTime == song.TrackTime && this.Genre == song.Genre && this.Rating == song.Rating;
        }

        public bool Equals(Song other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Id.Equals(other.Id) && this.Title.Equals(other.Title) && this.Artist.Equals(other.Artist)
                   && this.TrackTime.Equals(other.TrackTime) && this.Genre.Equals(other.Genre)
                   && this.Rating.Equals(other.Rating);
        }

        public override sealed int GetHashCode()
        {
            return this.Title.GetHashCode() + this.Artist.GetHashCode() + this.TrackTime.GetHashCode()
                   + this.Genre.GetHashCode() + this.Rating.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                "[{0}] ID {1} Title: {2} Artist: {3} Genre: {4} Rating: {5}",
                this.TrackTime,
                this.Id,
                this.Title,
                this.Artist,
                this.Genre,
                this.Rating);
        }

        #endregion

        #region Explicit Interface Methods

        int IComparable<Song>.CompareTo(Song other)
        {
            return this.CompareTo(other);
        }

        bool IEquatable<Song>.Equals(Song other)
        {
            return this.Equals(other);
        }

        #endregion
    }
}