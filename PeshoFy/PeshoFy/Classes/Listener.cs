using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    class Listener : User
    {
       private List<Song> favoriteSongs;
       private List<PlayList> playLists;

        public Listener(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Song> favoriteSongs, List<PlayList> playLists) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.FavoriteSongs = favoriteSongs;
            this.PlayLists = playLists;
        }

        public List<Song> FavoriteSongs { get => favoriteSongs; set => favoriteSongs = value; }
        public List<PlayList> PlayLists
        {
            get => playLists;
            set => playLists = value;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(String.Format("Playlists: \n"));

            if (PlayLists.Count == 0)
            {
                sb.Append("There are no playlists.\n");
            }
            else
            {
                int position = 1;

                foreach (PlayList playlist in PlayLists)
                {
                    sb.Append(String.Format("{0}. {1}\n", position, playlist.Name));
                    position++;
                }
            }

            return sb.ToString();
        }
        public void PrintMyPlayLists(string username)
        {
            StringBuilder sb = new StringBuilder();

            if (PlayLists.Count == 0)
            {
                sb.Append("It is an empty collection.\n");
            }
            else
            {
                int playlistPosition = 1;

                foreach (PlayList playlist in Storage.Listeners[username].PlayLists)
                {
                    if (playlist != null)
                    {
                        sb.Append(String.Format("{0}. Album - {1}\n", playlistPosition, playlist.Name));
                    }

                    if (Storage.Playlists[playlist.Name].Songs.Count == 0)
                    {
                        sb.Append("   There are no songs in the current album.\n");
                    }
                    else
                    {
                        int songsCount = Storage.Playlists[playlist.Name].Songs.Count;

                        sb.Append(String.Format("   There are {0} songs in this Album\n", songsCount));
                    }

                    playlistPosition++;
                }
            }

            Console.WriteLine(sb.ToString());
        }
        public void PlaylistInfo(string playlistName)
        {
            StringBuilder sb = new StringBuilder();

            PlayList playlist = PlayLists.Find(playlist => playlist.Name == playlistName);

            if (playlist == null)
            {
                sb.Append("There are no Playlists with this name!\n");
            }
            else
            {
                sb.Append(String.Format("Playlist name: {0}\n", playlistName));

                PlayList currentPlaylist = Storage.Playlists[playlistName];
                
                sb.Append(currentPlaylist.GetDurationTime());
            }

            Console.Write("\n{0}", sb.ToString());
        }
        public void CreatePlayList(string name)
        {
                List<Song> songs = new List<Song>();

                PlayList playlistToReturn = new PlayList(name);

                PlayLists.Add(playlistToReturn);
        }
        public void GetPlayLists()
        {
            foreach (var playList in PlayLists)
            {
                string result = $"<playlists><" + playList + "</playlists>";
                Console.WriteLine(result);
            }
        }
    }
}
