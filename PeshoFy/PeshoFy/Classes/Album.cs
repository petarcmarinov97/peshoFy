using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Album : PlayList
    {
        private Artist artist;
        private string genre;
        private DateTime releaseDate;

        public Album(string name, string duration, List<Song> songs,Artist artist, string genre, DateTime releaseDate) : base(name, duration, songs)
        {
            this.Artist = artist;
            this.Genre = genre;
            this.releaseDate = releaseDate;
        }

        public string Genre { get => genre; set => genre = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public Artist Artist { get => artist; set => artist = value; }
    }
}
