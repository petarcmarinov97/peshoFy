using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    class ReadData
    {
        static Storage storage = new Storage();
        public static Storage Storage { get => storage; set => storage = value; }

        static string username = string.Empty;
        static string password = string.Empty;
        static string type = string.Empty;
        static string fullName = string.Empty;
        static string dateOfBirth = string.Empty;
        static string genresInputString;
        static string likedSongsString;
        static string playListsInputString;
        static string albumsInput;
        public static bool LoginCheck(string usernameCheck, string passwordCheck) //Calls on Login to Auth the user
        {
            //<user><petarcm97>(123456){listener}</user>
            Regex userRegex = new Regex("(?<=<user>)<(?<username>\\S+)>\\((?<password>.+)\\){(?<type>[a-z]+)}(?=<\\/user>)");
            bool result = false;

            foreach (string line in File.ReadLines(Constants.FILE_PATH))
            {
                if (line != null)
                {
                    if (userRegex.IsMatch(line))
                    {
                        username = userRegex.Match(line).Groups["username"].Value;
                        password = userRegex.Match(line).Groups["password"].Value;
                        type = userRegex.Match(line).Groups["type"].Value;

                        if (usernameCheck == username && passwordCheck == password)
                        {
                            Storage.UserTypes.Add(username, type);
                            result = true;
                            break;
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public static void ReadListener()
        {
            //<listener><Go6koy><Georgi D>[17/12/1996](genres: ['rock', 'metal'])(likedSongs: ['Nothing Else Matters', 'Obseben'])(playlists: [])</listener>
            Regex listenerRegex = new Regex("(?<=<listener>)<(?<username>\\S+)><(?<fullName>\\D+)>\\[(?<dateOfBirth>\\d+\\/\\d+\\/\\d+)\\]\\(genres: \\[(?<genres>.*?)\\]\\)\\(likedSongs: \\[(?<likedSongs>.*?)\\]\\)\\(playlists: \\[(?<playlists>.*?)\\]\\)(?=<\\/listener>)");
            foreach (string line in File.ReadLines(Constants.DataBase_PATH).Where(line => listenerRegex.IsMatch(line)))
            {
                username = listenerRegex.Match(line).Groups["username"].Value;
                fullName = listenerRegex.Match(line).Groups["fullName"].Value;
                dateOfBirth = listenerRegex.Match(line).Groups["dateOfBirth"].Value;
                genresInputString = listenerRegex.Match(line).Groups["genres"].Value.Replace("\'", "");
                likedSongsString = listenerRegex.Match(line).Groups["likedSongs"].Value.Replace("\'", "");
                playListsInputString = listenerRegex.Match(line).Groups["playlists"].Value.Replace("\'", "");

                List<string> genresNames = genresInputString.Split(", ").ToList<string>();
                List<string> songsNames = likedSongsString.Split(", ").ToList();
                List<string> playListsNames = playListsInputString.Split(", ").ToList();

                List<Song> favoriteSongs = new List<Song>();
                List<PlayList> playLists = new List<PlayList>();

                Listener listener = new Listener(username, password, fullName, dateOfBirth, genresNames, favoriteSongs, playLists);
                Storage.Listeners.Add(username, listener);
                
                /*foreach (string name in playListsNames)
                {
                    PlayList playlisttoAdd = new PlayList(name);
                    playLists.Add(playlisttoAdd);
                }*/

                /*foreach (string name in songsNames)
                {
                    if (name != "")
                    {
                        Song song = new Song(name);
                        if (!favoriteSongs.Contains(song))
                        {
                            favoriteSongs.Add(song);
                        }
                    }
                }*/
            }
        }
        public static void ReadArtist()
        {
            //<artist><Metallica><Metallica>[28/10/1981](genres: ['rock', 'metal'])(albums: ['Black Album'])</artist>
            Regex artistRegex = new Regex("(?<=<artist>)<(?<username>\\S+)><(?<fullName>\\D+ [A-Z][a-z]+|[A-Z][a-z]+)>\\[(?<dateOfBirth>\\d+\\/\\d+\\/\\d+)\\]\\(genres: \\[(?<genres>.*?)\\]\\)\\(albums: \\[(?<albums>.*?)\\]\\)(?=<\\/artist>)");
            foreach (string line in File.ReadLines(Constants.DataBase_PATH).Where(line => artistRegex.IsMatch(line)))
            {
                username = artistRegex.Match(line).Groups["username"].Value;
                fullName = artistRegex.Match(line).Groups["fullName"].Value;
                dateOfBirth = artistRegex.Match(line).Groups["dateOfBirth"].Value;
                genresInputString = artistRegex.Match(line).Groups["genres"].Value.Replace("\'", "");
                albumsInput = artistRegex.Match(line).Groups["albums"].Value.Replace("\'", "");

                List<string> genres = genresInputString.Split(", ").ToList<string>();
                List<string> albumsList = albumsInput.Split(", ").ToList<string>();

                List<Album> albums = new List<Album>();
                foreach (string name in albumsList)
                {
                    if (name != "")
                    {
                        Album album = new Album(name);
                        if (!albums.Contains(album))
                        {
                            albums.Add(album);
                        }
                    }
                }

                Artist artist = new Artist(username, password, fullName, dateOfBirth, genres, albums);

                Storage.Artists.Add(username, artist);
            }
        }

        public static void ShowDisplayByType()
        {
            if (type == "artist")
            {
                MenuController.ArtistDisplay();
            }

            if (type == "listener")
            {
                MenuController.ListenerDisplay();
                TasksListeners();
            }
        }
        public static void TasksListeners()
        {
            int input = int.Parse(Console.ReadLine());


            if (input == 1)
            {
                PrintUserInfo(username);
            }

            if (input == 2)
            {
                Storage.Listeners[username].GetPlayLists();
            }
        }
        public static void PrintUserInfo(string username)
        {
            Storage.PrintInfo(username);
        }
    }
}
