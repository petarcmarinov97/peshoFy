using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Artist : User
    {
        private List<Album> albums;
        public Artist(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Album> albums) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.Albums = albums;
        }
        public List<Album> Albums { get => albums; set => albums = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(String.Format("Albums: \n"));

            if (Albums.Count == 0)
            {
                sb.Append("There are no albums.\n");
            }
            else
            {
                int position = 1;

                foreach (Album album in Albums)
                {
                    sb.Append(String.Format("{0}. {1}\n", position, album.Name));
                    position++;
                }
            }

            return sb.ToString();
        }

        public void PrintMyAlbums()
        {
            StringBuilder sb = new StringBuilder();

            if (Albums.Count == 0)
            {
                sb.Append("It is an empty collection.\n");
            }
            else
            {
                int albumPosition = 1;

                foreach (Album album in Storage.Artists[this.Username].Albums)
                {
                    if (album != null)
                    {
                        sb.Append(String.Format("{0}. Album - {1}\n", albumPosition, album.Name));
                    }

                    if (Storage.Albums[album.Name].Songs.Count == 0)
                    {
                        sb.Append("   There are no songs in the current album.\n");
                    }
                    else
                    {
                        int songsCount = Storage.Albums[album.Name].Songs.Count;

                        sb.Append(String.Format("   There are {0} songs in this Album\n", songsCount));
                    }

                    albumPosition++;
                }
            }

            Console.WriteLine(sb.ToString());
        }
        public void PrintAlbumInfo(string albumName)
        {
            StringBuilder sb = new StringBuilder();

            Album album = Albums.Find(album => album.Name == albumName);

            if (album == null)
            {
                sb.Append("There are no Albums with this name!\n");
            }
            else
            {
                sb.Append(String.Format("Album name: {0}\n", albumName));

                Album currentAlbum = Storage.Albums[albumName];

                sb.Append(currentAlbum.GetDurationTime());
            }

            Console.Write("\n{0}", sb.ToString());
        }
        public Album CreateAlbum(string albumName, List<string> albumGenres, string albumYear)
        {
            Album album = Albums.Find(album => album.Name == albumName);

            if (album == null)
            {
                List<Song> songs = new List<Song>();
                Artist artist = Storage.Artists[this.Username];
                Album albumToReturn = new Album(albumName, "", songs, artist, albumGenres, albumYear);

                return albumToReturn;
            }
            else
            {
                Console.WriteLine("Album already exists!");
            }

            return null;
        }
        public void DeleteAlbum(string albumName)
        {
            Album album = Albums.Find(album => album.Name == albumName);

            if (album == null)
            {
                Console.WriteLine("Album with this name does not exist!");
            }
            else
            {
                Console.WriteLine("Album has been removed succesfully");
                Albums.Remove(album);
            }
        }
        public void AddSongsToAlbum(Song songToAdd, string albumName)
        {
            Album album = Storage.Albums[albumName];

            if (album == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                album.AddSong(songToAdd);
            }
        }
        public void RemoveSongsFromAlbum(Song songToRemove, string albumName)
        {
            Album album = Storage.Albums[albumName];

            if (album == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                album.RemoveSong(songToRemove);
            }
        }
    }
}
