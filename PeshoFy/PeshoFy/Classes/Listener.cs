using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    class Listener : User
    {
        List<Song> favoriteSongs;
        List<PlayList> playLists;

        public Listener(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Song> favoriteSongs, List<PlayList> playLists) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.FavoriteSongs = favoriteSongs;
            this.PlayLists = playLists;
        }

        public List<Song> FavoriteSongs { get => favoriteSongs; set => favoriteSongs = value; }
        public List<PlayList> PlayLists { get => playLists; set => playLists = value; }

        public PlayList CreatePlayList(string name)
        {
            PlayList playList = PlayLists.Find(pl => pl.Name == name);

            if(playList == null)
            {
                List<Song> songs = new List<Song>();
                PlayList playlistToReturn = new PlayList(name, "", songs);

                return playlistToReturn;
            }
            else
            {
                Console.WriteLine("Playlist already exists!");
            }

            return null;
        }
        public void GetPlayLists()
        {
            foreach (PlayList playList in playLists)
            {
                string result = $"<playlists><" + playList.Name + $"</playlists>"; ;
                Console.WriteLine(result);
            }
        }
    }
}
