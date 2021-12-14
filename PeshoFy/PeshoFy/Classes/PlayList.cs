using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class PlayList : Content, ISongsContainer
    {
        private List<Song> songs;
        public PlayList(string name, string duration, List<Song> songs) : base(name, duration)
        {
            this.Songs = songs;
        }
        public List<Song> Songs
        {
            get => songs;
            set => songs = value.Count > 0 ? value : null;
        }
    }
}
