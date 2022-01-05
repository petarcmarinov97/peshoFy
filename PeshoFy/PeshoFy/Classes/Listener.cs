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

            if (this.PlayLists.Count == 0)
            {
                sb.Append("   There are no playlists.\n");
            }
            else
            {
                int position = 1;

                foreach (PlayList playlist in PlayLists)
                {
                    sb.Append(String.Format("   {0}. {1}\n", position, playlist.Name));
                    position++;
                }
            }

            sb.Append(String.Format("Favorites Songs: \n"));
            
            if(this.FavoriteSongs.Count == 0)
            {
                sb.Append("   There are no favorite songs.\n");
            }
            else
            {
                int position = 1;

                foreach (Song song in this.FavoriteSongs)
                {
                    sb.Append(String.Format("   {0}. {1}\n", position, song.Name));
                    position++;
                }
            }

            return sb.ToString();
        }
        public void PrintMyPlaylists()
        {
            StringBuilder sb = new StringBuilder();

            if (PlayLists.Count == 0)
            {
                sb.Append("It is an empty collection.\n");
            }
            else
            {
                int playlistPosition = 1;

                foreach (PlayList playlist in Storage.Listeners[this.Username].PlayLists)
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
        public void PrintPlaylistInfo(string playlistName)
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
        public void PrintFavoriteSongs()
        {
            StringBuilder sb = new StringBuilder();

            if (this.FavoriteSongs.Count == 0)
            {
                sb.Append("   There are no favorite songs.\n");
            }
            else
            {
                int songsCount = 1;
                foreach (Song song in this.FavoriteSongs)
                {
                    if (song != null)
                    {
                        sb.Append(String.Format("   {0}. {1}\n", songsCount, song.Name));
                    }
                    songsCount++;
                }
            }

            Console.WriteLine(sb.ToString());
        }
        public PlayList CreatePlayList(string name, List<string> genres)
        {
            PlayList playlist = PlayLists.Find(playlist => playlist.Name == name);

            if (playlist == null)
            {
                List<Song> songs = new List<Song>();
                Listener listener = Storage.Listeners[this.Username];
                PlayList playlistToReturn = new PlayList(name, "", songs, listener, genres);

                return playlistToReturn;
            }
            else
            {
                Console.WriteLine("Playlist already exists!");
            }

            return null;
        }
        public void DeletePlayList(string name)
        {
            PlayList playlist = PlayLists.Find(playlist => playlist.Name == name);

            if (playlist == null)
            {
                Console.WriteLine("Album with this name does not exist!");
            }
            else
            {
                Console.WriteLine("Album has been removed succesfully");
                PlayLists.Remove(playlist);
            }
        }
        public void AddSongsToPlaylist(Song songtoAdd, string playlistName)
        {
            PlayList playlist = Storage.Playlists[playlistName];

            if (playlist == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                playlist.AddSong(songtoAdd);
            }
        }
        public void RemoveSongsFromPlaylist(Song songToRemove, string playlistName)
        {
            PlayList playlist = Storage.Playlists[playlistName];

            if (playlist == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                playlist.RemoveSong(songToRemove);
            }
        }
        public void AddSongsToFavorites(Song songToAdd)
        {
            if (FavoriteSongs.Contains(songToAdd))
            {
                Console.WriteLine("Song {0} is already in the playlist", songToAdd.Name);
            }
            else
            {
                FavoriteSongs.Add(songToAdd);
                Console.WriteLine("Song {0} has been added to favorites!", songToAdd.Name);
            }
        }
        public void RemoveSongsFromFavorites(string songToRemove)
        {
            var songToDelete = FavoriteSongs.Find(x => x.Name == songToRemove);
            if (songToDelete != null)
            {
                FavoriteSongs.Remove(songToDelete);
                Console.WriteLine("The song {0} has been removed successfully from favorites", songToRemove);
            }
            else
            {
                Console.WriteLine("The song does not exist in the favorites collection!");
            }
        }
    }
}
