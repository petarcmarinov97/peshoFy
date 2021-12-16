using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Song : Content
    {
        private string album;
        private string genre;
        private string releaseDate;
        private Artist artist;

        public Song(string name) : base(name)
        {

        }
        public Song(string name, string duration, string album, Artist artist, string genre, string releaseDate) : base(name, duration)
        {
            this.Album = album;
            this.Artist = artist;
            this.Genre = genre;
            this.ReleaseDate = releaseDate;
        }
        public string Album { get => album; set => album = value; }
        public string Genre { get => genre; set => genre = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public Artist Artist { get => artist; set => artist = value; }
    }
}
