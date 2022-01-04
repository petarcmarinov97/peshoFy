using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PeshoFy.Classes
{
    class FileReader
    {
        private string username = string.Empty;
        private string password = string.Empty;
        private string type = string.Empty;
        private string fullName = string.Empty;
        private string dateOfBirth = string.Empty;
        private string genresInputString;
        private string likedSongsString;
        private string playListsInputString;
        private string albumsInput;

        public void ReadFile()
        {
            //<listener><drago>(123)<D Ivanov>[14/10/1997](genres: [])(likedSongs: [])(playlists: ['Obseben', 'Yarost', 'Ebacha', 'Tigura])</listener>
            Regex listenerRegex = new Regex("(?<=<listener>)<(?<username>\\S+)><(?<password>\\S+)><(?<fullName>\\D+)>\\[(?<dateOfBirth>\\d+\\/\\d+\\/\\d+)\\]\\(genres: \\[(?<genres>.*?)\\]\\)\\(likedSongs: \\[(?<likedSongs>.*?)\\]\\)\\(playlists: \\[(?<playlists>.*?)\\]\\)(?=<\\/listener>)");

            //<artist><Stenli>(123456)<Stanislav Slanev>[12/8/1959](genres: ['pop', 'rock'])(albums: ['Obseben'])
            Regex artistRegex = new Regex("(?<=<artist>)<(?<username>\\S+)><(?<password>\\S+)><(?<fullName>\\D+ [A-Z][a-z]+|[A-Z][a-z]+)>\\[(?<dateOfBirth>\\d+\\/\\d+\\/\\d+)\\]\\(genres: \\[(?<genres>.*?)\\]\\)\\(albums: \\[(?<albums>.*?)\\]\\)(?=<\\/artist>)");

            //<song><Nothing Else Matters>[6:28]</song>
            Regex songRegex = new Regex("(?<=<song>)<(?<name>.+)>\\[(?<length>[0-9]{1,}:[0-9]{2})\\](?=<\\/song)");

            //<album><Black Album>[1991](genres: [metal])(songs: ['Enter Sandman', 'Sad but True', 'Nothing Else Matters'])</album>
            Regex albumRegex = new Regex("(?<=<album>)<(?<albumName>.*?)>\\[(?<year>\\d{4})\\]\\(genres: \\[(?<genres>.*?)\\]\\)\\(songs: \\[(?<songs>.*?)\\]\\)(?=<\\/album>)");

            //<playlists><Black Playlist>(songs: ['Yarost', 'Obseben'])</playlists>
            Regex playlistRegex = new Regex("(?<=<playlists>)<(?<playlistName>.*?)>\\(songs: \\[(?<songs>.*?)\\]\\)(?=<\\/playlists>)");

            foreach (string line in File.ReadLines(Constants.FILE_PATH))
            {
                if (line != null)
                {
                    if (listenerRegex.IsMatch(line)) //Check the line for listener
                    {
                        type = "listener";
                        username = listenerRegex.Match(line).Groups["username"].Value;
                        password = listenerRegex.Match(line).Groups["password"].Value;
                        fullName = listenerRegex.Match(line).Groups["fullName"].Value;
                        dateOfBirth = listenerRegex.Match(line).Groups["dateOfBirth"].Value;
                        genresInputString = listenerRegex.Match(line).Groups["genres"].Value.Replace("\'", "");
                        likedSongsString = listenerRegex.Match(line).Groups["likedSongs"].Value.Replace("\'", "");
                        playListsInputString = listenerRegex.Match(line).Groups["playlists"].Value.Replace("\'", "");

                        List<string> genresNames = genresInputString.Split(", ").ToList<string>();
                        List<string> songsNames = likedSongsString.Split(", ").ToList();
                        List<string> playListsNames = playListsInputString.Split(", ").ToList();

                        List<Song> favoriteSongs = new List<Song>();
                        foreach (string song in songsNames)
                        {
                            if (song != "")
                            {
                                Song songtoAdd = new Song(song);
                                if (favoriteSongs.Contains(songtoAdd))
                                {
                                    favoriteSongs.Add(songtoAdd);
                                }
                            }
                        }

                        List<PlayList> playLists = new List<PlayList>();
                        foreach (string playlistName in playListsNames)
                        {
                            if (playlistName != "")
                            {
                                PlayList playlistToAdd = new PlayList(playlistName);
                                if (!playLists.Contains(playlistToAdd))
                                {
                                    playLists.Add(playlistToAdd);
                                }
                            }
                        }

                        Listener listener = new Listener(username, password, fullName, dateOfBirth, genresNames, favoriteSongs, playLists);

                        Storage.Listeners.Add(username, listener);
                        Storage.UserTypes.Add(username, type);
                    }
                    else if (artistRegex.IsMatch(line)) //Check the line for artist
                    {
                        type = "artist";
                        username = artistRegex.Match(line).Groups["username"].Value;
                        password = artistRegex.Match(line).Groups["password"].Value;
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
                        Storage.UserTypes.Add(username, type);
                    }
                    else if (albumRegex.IsMatch(line)) //Check the line for album
                    {
                        string albumName = albumRegex.Match(line).Groups["albumName"].Value;
                        string year = albumRegex.Match(line).Groups["year"].Value;
                        string genresInputString = albumRegex.Match(line).Groups["genres"].Value.Replace("\'", "");
                        string songsInputString = albumRegex.Match(line).Groups["songs"].Value.Replace("\'", "");

                        List<string> genres = genresInputString.Split(", ").ToList<string>();
                        List<string> songs = songsInputString.Split(", ").ToList<string>();

                        if (!Storage.Albums.ContainsKey(albumName))
                        {
                            Album album = new Album(albumName);
                            Storage.Albums[albumName] = album;
                        }

                        Storage.Albums[albumName].ReleaseDate = year;

                        foreach (string name in genres)
                        {
                            if (name != "")
                            {
                                if (Storage.Albums[albumName].Genres == null)
                                {
                                    List<string> genresList = new List<string>();
                                    Storage.Albums[albumName].Genres = genresList;
                                    Storage.Albums[albumName].Genres.Add(name);
                                }
                                else
                                {
                                    Storage.Albums[albumName].Genres.Add(name);
                                }
                            }
                        }

                        foreach (string name in songs)
                        {
                            if (name != "")
                            {
                                if (Storage.Songs.ContainsKey(name))
                                {
                                    Storage.Albums[albumName].Songs.Add(Storage.Songs[name]);
                                }
                                else
                                {
                                    Song song = new Song(name);
                                    Storage.Songs.Add(name, song);
                                    Storage.Albums[albumName].Songs.Add(song);
                                }

                                Storage.Songs[name].Artist = Storage.Albums[albumName].Artist;
                                Storage.Songs[name].Album = Storage.Albums[albumName];
                                Storage.Songs[name].ReleaseDate = Storage.Albums[albumName].ReleaseDate;
                            }
                        }
                    }
                    else if (songRegex.IsMatch(line)) //Check the line for song
                    {
                        string name = songRegex.Match(line).Groups["name"].Value;
                        string length = songRegex.Match(line).Groups["length"].Value;

                        if (!Storage.Songs.ContainsKey(name))
                        {
                            Song song = new Song(name, length);
                            Storage.Songs.Add(name, song);
                        }
                        Storage.Songs[name].ReleaseDate = "";
                        Storage.Songs[name].Genre = "";
                        Storage.Songs[name].Duration = length;
                    }
                    else if (playlistRegex.IsMatch(line)) //Check the line for playlist
                    {
                        string name = playlistRegex.Match(line).Groups["playlistName"].Value;
                        string songsInputString = playlistRegex.Match(line).Groups["songs"].Value.Replace("\'", "");

                        List<string> songs = songsInputString.Split(", ").ToList<string>();

                        if (!Storage.Playlists.ContainsKey(name))
                        {
                            PlayList playlist = new PlayList(name);
                            Storage.Playlists.Add(name, playlist);
                        }
                        Storage.Playlists[name].Duration = "";

                        foreach (string songName in songs)
                        {
                            Storage.Playlists[name].Songs.Add(Storage.Songs[songName]);
                        }
                    }
                }
            }
        }
    }
}
