using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Album : Content, ISongsContainer
    {
        private Artist artist;
        private string genre;
        private string releaseDate;
        private List<Song> songs;
        public Album(string name) : base(name)
        {
        }
        public Album(string name, string duration, List<Song> songs, Artist artist, string genre, string releaseDate) : base(name, duration)
        {
            this.Genre = genre;
            this.releaseDate = releaseDate;
            this.Songs = songs;
            this.Artist = artist;
        }
        public Artist Artist { get => artist; set => artist = value; }
        public string Genre { get => genre; set => genre = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public List<Song> Songs
        {
            get => songs;
            set => songs = value.Count > 0 ? value : null;
        }
    }
}
