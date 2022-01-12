using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    public class Listener : User
    {
        private List<Song> favoriteSongs;
        private List<PlayList> playLists;

        public Listener(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Song> favoriteSongs, List<PlayList> playLists) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.FavoriteSongs = favoriteSongs;
            this.PlayLists = playLists;
            this.Genres = genres;
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

            if (this.FavoriteSongs.Count == 0)
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

        public override string PrintCollection(Constants.typeCollection type)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case Constants.typeCollection.playlists:
                    if (PlayLists.Count == 0)
                    {
                        sb.Append("It is an empty collection.\n");
                    }
                    else
                    {
                        int playlistPosition = 1;

                        foreach (PlayList playlist in this.PlayLists)
                        {
                            if (playlist != null)
                            {
                                sb.Append(String.Format("{0}. Playlist - {1}\n", playlistPosition, playlist.Name));
                            }

                            if (playlist.Songs.Count == 0)
                            {
                                sb.Append("   There are no songs in the current playlist.\n");
                            }
                            else
                            {
                                int songsCount = playlist.Songs.Count;

                                sb.Append(String.Format("   There are {0} songs in this Playlist\n", songsCount));
                            }

                            playlistPosition++;
                        }
                    }

                    break;

                case Constants.typeCollection.favorites:
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

                    break;
            }

            return sb.ToString();
        }

        public override string PrintCollectionInfo(string playlistName)
        {
            StringBuilder sb = new StringBuilder();

            if (PlayLists.Find(playlist => playlist.Name == playlistName) == null)
            {
                sb.Append("There are no Playlists with this name!\n");
            }
            else
            {
                PlayList currentPlaylist = Storage.Playlists[playlistName];
                sb.Append(currentPlaylist.GetInfo());
            }

            Console.Write("\n{0}", sb.ToString());
            return sb.ToString();
        }

        public PlayList CreatePlayList(string name, List<string> genres, List<Song> songsToAdd)
        {
            return new PlayList(name, songsToAdd, this, genres);
        }

        public void DeletePlayList(string name)
        {
            this.PlayLists.Remove(this.PlayLists.Find(playlist => playlist.Name == name));
        }

        public void AddSongsToPlaylist(Song songtoAdd, string playlistName)
        {
            var currentPlaylist = this.playLists.Find(playlist => playlist.Name == playlistName);

            if (currentPlaylist.Songs.Count == 0 || !currentPlaylist.Songs.Contains(songtoAdd))
            {
                currentPlaylist.AddSong(songtoAdd);
                Console.WriteLine("The song {0} has been added to the Playlist\n", songtoAdd.Name);
            }
            else
            {
                Console.WriteLine("The song {0} is already in this Playlist!\n", songtoAdd.Name);
            }
        }

        public void RemoveSongsFromPlaylist(Song songToRemove, string playlistName)
        {
            var currentplaylist = this.playLists.Find(playlist => playlist.Name == playlistName);

            if (currentplaylist.Songs.Count == 0)
            {
                Console.WriteLine("There is no Songs in the Playlist!\n");
            }
            else if (currentplaylist.Songs.Contains(songToRemove))
            {
                currentplaylist.RemoveSong(songToRemove);
                Console.WriteLine("The song {0} has been removed from the Playlist\n", songToRemove.Name);
            }
            else
            {
                Console.WriteLine("A song with this name do not exist in the Playlist!\n");
            }
        }

        public void AddSongsToFavorites(Song songToAdd)
        {
            if (FavoriteSongs.Contains(songToAdd))
            {
                Console.WriteLine("Song {0} is already in Favorites", songToAdd.Name);
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
