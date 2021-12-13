using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Listener : Person
    {
        private List<Song> favoriteSongs;
        private List<PlayList> listOfPlayLists;
        public Listener(string username, string password, string fullName, DateTime dateOfBirth, List<string> genres, List<Song> favoriteSongs, List<PlayList> listOfPlayLists) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.FavoriteSongs = favoriteSongs;
            this.ListOfPlayLists = listOfPlayLists;
        }

        public List<Song> FavoriteSongs { get => favoriteSongs; set => favoriteSongs = value; }
        public List<PlayList> ListOfPlayLists { get => listOfPlayLists; set => listOfPlayLists = value; }

        public string GetInfo()
        {
            string favoriteSongs= string.Empty;
            foreach(Song song in FavoriteSongs)
            {
                favoriteSongs+=song.Name+"\n";
            }

            var listOfPlayLists = string.Empty;
            foreach(PlayList playList in ListOfPlayLists)
            {
                listOfPlayLists += playList.Name + "\n";
            }
            return string.Join("\n", Username, FullName, DateOfBirth, string.Join(" ", Genres), string.Join(" ", favoriteSongs), string.Join(" ", listOfPlayLists));
        }
    }
}
