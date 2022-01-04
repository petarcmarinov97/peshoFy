using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Album : Content, ISongsContainer
    {
        private Artist artist;
        private string releaseDate;
        private List<string> genres;
        private List<Song> songs = new List<Song>();
        public Album(string name) : base(name)
        {
        }
        public Album(string name, string duration, List<Song> songs, Artist artist, List<string> genres, string releaseDate) : base(name, duration)
        {
            this.Artist = artist;
            this.Genres = genres;
            this.releaseDate = releaseDate;
            this.Songs = songs;
        }
        public Artist Artist { get => artist; set => artist = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public List<Song> Songs { get => songs; set => songs = value; }
        public List<string> Genres { get => genres; set => genres = value; }
        public override string GetDurationTime()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Year: {0}\n", this.ReleaseDate));
            sb.Append("Genres: ");

            foreach (string genre in this.Genres)
            {
                sb.Append(String.Format("{0} ", genre));
            }

            if (this.Songs.Count == 0)
            {
                sb.Append("There are no songs in the Album!\n");
            }
            else
            {
                int songCounter = 1;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                string outputHours = string.Empty;
                string outputMinutes = string.Empty;
                string outputSeconds = string.Empty;

                sb.Append(String.Format("Songs in the album:\n"));
                foreach (Song song in this.Songs)
                {
                    sb.Append(String.Format("   {0}. {1}\n", songCounter, song.Name));

                    string[] songData = song.Duration.Split(":");

                    if (songData.Length == 3)
                    {
                        seconds += int.Parse(songData[2]);
                        if (seconds > 59)
                        {
                            minutes++;
                            seconds -= 60;
                        }

                        minutes += int.Parse(songData[1]);
                        if (minutes > 59)
                        {
                            hours++;
                            minutes -= 60;
                        }

                        hours += int.Parse(songData[0]);
                    }
                    else
                    {
                        seconds += int.Parse(songData[1]);
                        if (seconds > 59)
                        {
                            minutes++;
                            seconds -= 60;
                        }
                        minutes += int.Parse(songData[0]);
                        if (minutes > 59)
                        {
                            hours++;
                            minutes -= 60;
                        }
                    }
                }

                outputHours = hours.ToString();
                outputMinutes = minutes.ToString();
                outputSeconds = seconds.ToString();

                if (hours < 10)
                {
                    outputHours = "0" + hours.ToString();
                }
                if (minutes < 10)
                {
                    outputMinutes = "0" + minutes.ToString();
                }
                if (seconds < 10)
                {
                    outputSeconds = "0" + seconds.ToString();
                }

                sb.Append(String.Format("Album Duration: {0}:{1}:{2}\n", outputHours, outputMinutes, outputSeconds));
                this.Duration = String.Format("{0}:{1}:{2}\n", outputHours, outputMinutes, outputSeconds);
            }

            return sb.ToString();
        }
        public void AddSong(Song songToAdd)
        {
            if (Songs.Count == 0)
            {
                Songs.Add(songToAdd);
                Console.WriteLine("The song {0} has been added to the Album\n", songToAdd.Name);
            }
            else
            {
                if (Songs.Contains(songToAdd))
                {
                    Console.WriteLine("The song {0} is already in this Album!\n", songToAdd.Name);
                }
                else
                {
                    Songs.Add(songToAdd);
                    Console.WriteLine("The song {0} has been added to the Album\n", songToAdd.Name);
                }
            }
        }
        public void RemoveSong(Song songToRemove)
        {
            if (Songs.Count == 0)
            {
                Console.WriteLine("There is no Songs in the Album!\n");
            }
            else
            {
                if (Songs.Contains(songToRemove))
                {
                    Songs.Remove(songToRemove);
                    Console.WriteLine("The song {0} has been removed from the Album\n", songToRemove.Name);
                }
                else
                {
                    Console.WriteLine("A song with this name do not exist in the Album!\n");
                }
            }
        }

    }
}
