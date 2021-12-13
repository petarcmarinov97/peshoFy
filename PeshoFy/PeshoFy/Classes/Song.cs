using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Song : Content
    {
        private Album album;
        private Artist artist;
        private string genre;
        private DateTime releaseDate;

        public Song(string name, string duration, Album album, Artist artist, string genre, DateTime releaseDate) : base(name, duration)
        {
            this.Album = album;
            this.Artist = artist;
            this.Genre = genre;
            this.ReleaseDate = releaseDate;
        }

        public string Genre { get => genre; set => genre = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        internal Album Album { get => album; set => album = value; }
        internal Artist Artist { get => artist; set => artist = value; }
    }
}
