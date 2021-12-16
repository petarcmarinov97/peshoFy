using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    class PlayList : Content, ISongsContainer
    {
        private static List<Song> songs;
        public PlayList(string name) : base(name)
        {
        }

        public PlayList(string name, string duration, List<Song> songs) : base(name, duration)
        {
            
            this.Songs = songs;
        }

        public List<Song> Songs
        {
            get => songs;
            set => songs = value;
        }
    }
}
