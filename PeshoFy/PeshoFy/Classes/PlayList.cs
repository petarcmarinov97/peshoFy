using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class PlayList : Content
    {
        private List<Song> songs;
        public PlayList(string name, string duration, List<Song> songs) : base(name, duration)
        {
            this.songs = songs;
        }
        internal List<Song> Songs { get => songs; set => songs = value; }
    }
}
