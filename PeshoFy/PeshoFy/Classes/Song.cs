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
        private string releaseDate;

        public Song(string name) : base(name)
        {
        }
        public Song(string name, string duration) : base(name, duration)
        {
        }
        public Song(string name, string duration, Album album, Artist artist, string genre, string releaseDate) : base(name, duration)
        {
            this.Album = album;
            this.Artist = artist;
            this.Genre = genre;
            this.ReleaseDate = releaseDate;
        }
        public Album Album { get => album; set => album = value; }
        public Artist Artist { get => artist; set => artist = value; }
        public string Genre { get => genre; set => genre = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
    }
}
