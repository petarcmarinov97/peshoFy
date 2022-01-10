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
            this.Genres = genres;
        }

        public List<Album> Albums { get => albums; set => albums = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(String.Format("Albums: \n"));

            if (this.Albums.Count == 0)
            {
                sb.Append("   There are no albums.\n");
            }
            else
            {
                int position = 1;

                foreach (Album album in this.Albums)
                {
                    sb.Append(String.Format("   {0}. {1}\n", position, album.Name));
                    position++;
                }
            }

            return sb.ToString();
        }

        public override void PrintCollection(Constants.typeCollection type)
        {
            StringBuilder sb = new StringBuilder();

            if (this.Albums.Count == 0)
            {
                sb.Append("It is an empty collection.\n");
            }
            else
            {
                int albumPosition = 1;

                foreach (Album album in this.Albums)
                {
                    if (album != null)
                    {
                        sb.Append(String.Format("{0}. Album - {1}\n", albumPosition, album.Name));
                    }

                    if (album.Songs.Count == 0)
                    {
                        sb.Append("   There are no songs in the current album.\n");
                    }
                    else
                    {
                        int songsCount = album.Songs.Count;

                        sb.Append(String.Format("   There are {0} songs in this Album\n", songsCount));
                    }

                    albumPosition++;
                }
            }

            Console.WriteLine(sb.ToString());
        }

        public override void PrintCollectionInfo(string albumName)
        {
            StringBuilder sb = new StringBuilder();

            if (Albums.Find(album => album.Name == albumName) == null)
            {
                sb.Append("There are no Albums with this name!\n");
            }
            else
            {
                Album currentAlbum = Storage.Albums[albumName];
                sb.Append(currentAlbum.GetInfo());
            }

            Console.Write("\n{0}", sb.ToString());
        }

        public Album CreateAlbum(string name, List<string> genres, string year)
        {
            if (Albums.Find(album => album.Name == name) == null)
            {
                Album albumToReturn = new Album(name, new List<Song>(), this, genres, year);

                Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                string[] songsToAdd = Console.ReadLine().Split(", ");

                foreach (string song in songsToAdd)
                {
                    if (Storage.Songs.ContainsKey(song))
                    {
                        albumToReturn.Songs.Add(Storage.Songs[song]);
                    }
                    else
                    {
                        Console.WriteLine("Song as {0} does not exists!", song);
                    }
                }

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
            if (this.Albums.Find(album => album.Name == albumName) == null)
            {
                Console.WriteLine("Album with this name does not exist!");
            }
            else
            {
                Console.WriteLine("Album has been removed succesfully");
                this.Albums.Remove(this.Albums.Find(album => album.Name == albumName));
            }
        }

        public void AddSongsToAlbum(Song songToAdd, string albumName)
        {
            if (this.Albums.Find(album => album.Name == albumName) == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                this.Albums.Find(album => album.Name == albumName).AddSong(songToAdd);
            }
        }

        public void RemoveSongsFromAlbum(Song songToRemove, string albumName)
        {
            if (this.Albums.Find(album => album.Name == albumName) == null)
            {
                Console.Write("There is no album with this name\n");
            }
            else
            {
                this.Albums.Find(album => album.Name == albumName).RemoveSong(songToRemove);
            }
        }
    }
}
