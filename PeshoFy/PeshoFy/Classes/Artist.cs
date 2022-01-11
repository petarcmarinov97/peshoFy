using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    public class Artist : User
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

        public Album CreateAlbum(string name, List<string> genres, string year, List<Song> songsToAdd)
        {
            return new Album(name, songsToAdd, this, genres, year);
        }

        public void DeleteAlbum(string albumName)
        {
            this.Albums.Remove(this.Albums.Find(album => album.Name == albumName));
        }

        public void AddSongsToAlbum(Song songToAdd, string albumName)
        {
            var currentAlbum = this.Albums.Find(album => album.Name == albumName);

            if (currentAlbum.Songs.Count == 0 || !currentAlbum.Songs.Contains(songToAdd))
            {
                currentAlbum.AddSong(songToAdd);
                Console.WriteLine("The song {0} has been added to the Album\n", songToAdd.Name);
            }
            else
            {
                Console.WriteLine("The song {0} is already in this Album!\n", songToAdd.Name);
            }
        }

        public void RemoveSongsFromAlbum(Song songToRemove, string albumName)
        {
            var currentAlbum = this.Albums.Find(album => album.Name == albumName);

            if (currentAlbum.Songs.Count == 0)
            {
                Console.WriteLine("There is no Songs in the Album!\n");
            }
            else if (currentAlbum.Songs.Contains(songToRemove))
            {
                currentAlbum.RemoveSong(songToRemove);
                Console.WriteLine("The song {0} has been removed from the Album\n", songToRemove.Name);
            }
            else
            {
                Console.WriteLine("A song with this name do not exist in the Album!\n");
            }
        }
    }
}
