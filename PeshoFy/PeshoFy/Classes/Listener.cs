using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Listener : User
    {
        private List<Song> favoriteSongs;
        private List<PlayList> playLists;
        public Listener(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Song> favoriteSongs, List<PlayList> playLists) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.FavoriteSongs = favoriteSongs;
            this.PlayLists = playLists;
        }

        public List<Song> FavoriteSongs { get => favoriteSongs; set => favoriteSongs = value; }
        public List<PlayList> PlayLists { get => playLists; set => playLists = value; }

        public void GetPlayLists()
        {
            foreach(PlayList playList in playLists)
            {
                string result = $"<playlists><" + playList.Name + $"</playlists>"; ;
                Console.WriteLine(result);
            }
        }
    }
}
