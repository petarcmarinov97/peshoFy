using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class PlayList : Content
    {
        private List<Song> songs = new List<Song>();
        private Listener listener;
        private string releaseDate;
        private List<string> genres;
        public PlayList(string name) : base(name)
        {
        }
        public PlayList(string name, string duration, List<Song> songs, Listener listener, List<string> genres, string releaseDate) : base(name, duration)
        {
            this.Listener = listener;
            this.Genres = genres;
            this.ReleaseDate = releaseDate;
            this.Songs = songs;
        }
        public Listener Listener { get => listener; set => listener = value; }
        public List<string> Genres { get => genres; set => genres = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public List<Song> Songs { get => songs; set => songs = value; }

        public override string GetDurationTime()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Songs.Count == 0)
            {
                sb.Append("There are no songs in the playlist!\n");
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

                sb.Append(String.Format("Songs in the playlist:\n"));
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

                sb.Append(String.Format("Playlist Duration: {0}:{1}:{2}\n", outputHours, outputMinutes, outputSeconds));
                this.Duration = String.Format("{0}:{1}:{2}\n", outputHours, outputMinutes, outputSeconds);
            }

            return sb.ToString();
        }
    }
}
