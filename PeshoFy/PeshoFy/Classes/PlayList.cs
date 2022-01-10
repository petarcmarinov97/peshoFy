using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class PlayList : Content, ISongsContainer
    {
        private Listener listener;
        private List<string> genres;
        private List<Song> songs = new List<Song>();
        public PlayList(string name) : base(name)
        {
        }
        public PlayList(string name, List<Song> songs, Listener listener, List<string> genres) : base(name)
        {
            this.Listener = listener;
            this.Genres = genres;
            this.Songs = songs;
        }
        public Listener Listener { get => listener; set => listener = value; }
        public List<string> Genres { get => genres; set => genres = value; }
        public List<Song> Songs { get => songs; set => songs = value; }

        public void AddSong(Song songToAdd)
        {
            if (Songs.Count == 0)
            {
                Songs.Add(songToAdd);
                Console.WriteLine("The song {0} has been added to the Playlist\n", songToAdd.Name);
            }
            else
            {
                if (Songs.Contains(songToAdd))
                {
                    Console.WriteLine("The song {0} is already in this Playlist!\n", songToAdd.Name);
                }
                else
                {
                    Songs.Add(songToAdd);
                    Console.WriteLine("The song {0} has been added to the Playlist\n", songToAdd.Name);
                }
            }
        }
        public void RemoveSong(Song songToRemove)
        {
            if (Songs.Count == 0)
            {
                Console.WriteLine("There is no Songs in the Playlist!\n");
            }
            else
            {
                if (Songs.Contains(songToRemove))
                {
                    Songs.Remove(songToRemove);
                    Console.WriteLine("The song {0} has been removed from the Playlist\n", songToRemove.Name);
                }
                else
                {
                    Console.WriteLine("A song with this name do not exist in the Playlist!\n");
                }
            }
        }
        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Playlist name: {0}\n", this.Name));
            sb.Append(GetGenresInfo());
            sb.Append(GetSongsInfo());

            return sb.ToString();
        }

        public string GetGenresInfo()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Genres: ");

            if(this.Genres != null)
            {
                foreach (string genre in this.Genres)
                {
                    result.Append(String.Format("{0} ", genre));
                }
            }
            else
            {
                result.Append("do not have genres");
            }

            return result.ToString();
        }

        public string GetSongsInfo()
        {
            StringBuilder result = new StringBuilder();

            if (this.Songs.Count == 0)
            {
                result.Append("\nThere are no songs in the Album!\n");
            }
            else
            {
                int songCounter = 1;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;

                result.Append(String.Format("\nSongs in the album:\n"));
                foreach (Song song in this.Songs)
                {
                    string[] songData = song.Duration.Split(":");

                    result.Append(String.Format("   {0}. {1}\n", songCounter, song.Name));
                    CalcsTime(ref songData, ref hours, ref minutes, ref seconds);
                    songCounter++;
                }

                result.Append(GetDurationResult(hours, minutes, seconds));
            }

            return result.ToString();
        }

        public void CalcsTime(ref string[] data, ref int hours, ref int minutes, ref int seconds)
        {
            if (data.Length == 3)
            {
                seconds += int.Parse(data[2]);
                if (seconds > 59)
                {
                    minutes++;
                    seconds -= 60;
                }

                minutes += int.Parse(data[1]);
                if (minutes > 59)
                {
                    hours++;
                    minutes -= 60;
                }

                hours += int.Parse(data[0]);
            }
            else
            {
                seconds += int.Parse(data[1]);
                if (seconds > 59)
                {
                    minutes++;
                    seconds -= 60;
                }
                minutes += int.Parse(data[0]);
                if (minutes > 59)
                {
                    hours++;
                    minutes -= 60;
                }
            }
        }

        public string GetDurationResult(int hours, int minutes, int seconds)
        {
            StringBuilder result = new StringBuilder();
            string outputHours = hours < 10 ? "0" + hours.ToString() : hours.ToString();
            string outputMinutes = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
            string outputSeconds = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

            result.Append(String.Format("Album Duration: {0}:{1}:{2}\n", outputHours, outputMinutes, outputSeconds));

            return result.ToString();
        }
    }
}
