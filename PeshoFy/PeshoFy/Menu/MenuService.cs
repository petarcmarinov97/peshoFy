using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PeshoFy.Classes
{
    static class MenuService
    {
        private static string welcome = string.Empty;
        private static string options = string.Empty;
        private static int input;
        private static string username;
        private static string accountType;
        private static FileWriter file = new FileWriter();

        public static void DisplayMenu()
        {
            ShowHeader();
            EnterOption();
            LoginDisplay();
        }

        public static void ShowHeader()
        {
            Console.WriteLine(Constants.WELCOME_MESSAGE);
            Console.WriteLine(Constants.OPTIONS_MESSAGE);
        }

        public static void EnterOption()
        {
            isValidInput(Constants.typeDisplay.choicesDisplay);
            switch (input)
            {
                case (int)Constants.acceptOptions.login:
                    Login.LoginUser();
                    break;
                case (int)Constants.acceptOptions.register:
                    Register.RegisterUser();
                    break;
                default:
                    Console.WriteLine(Constants.WRONG_COMMAND_MESSAGE);
                    ShowHeader();
                    EnterOption();
                    break;
            }
        }

        public static void LoginDisplay()
        {
            string[] userInfo = Login.GetAccountType();
            username = userInfo[0];
            accountType = userInfo[1];

            if (accountType == Constants.ARTIST)
            {
                PerformArtistActions();
            }
            else if (accountType == Constants.LISTENER)
            {
                PerformListenerActions();
            }
        }

        public static void PerformArtistActions()
        {
            Artist artist = Storage.Artists[username];
            ShowArtistDisplay();

            isValidInput(Constants.typeDisplay.artistDisplay);
            switch (input)
            {
                case (int)Constants.artistMenu.printInfoAboutMe:
                    Console.WriteLine(artist.ToString());
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.printMyAlbums:
                    artist.PrintCollection(Constants.typeCollection.albums);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.printAlbumInfo:
                    string album = WriteName(Constants.typeName.album);

                    artist.PrintCollectionInfo(album);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.createAlbum:
                    album = WriteName(Constants.typeName.album);
                    var albumYear = WriteYear();
                    var albumGenres = WriteGenres();

                    if (artist.Albums.Find(x => x.Name == album) == null)
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        var albumSongs = WriteSongs();

                        Album newAlbum = artist.CreateAlbum(album, albumGenres, albumYear, albumSongs);
                        SaveAlbum(artist, newAlbum);
                    }
                    else
                    {
                        Console.WriteLine("Album already exists!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.removeAlbum:
                    album = WriteName(Constants.typeName.album);

                    if (artist.Albums.Find(x => x.Name == album) == null)
                    {
                        Console.WriteLine("Album with this name does not exist!");
                    }
                    else
                    {
                        DeleteAlbum(artist, album);
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.addSongsToAlbum:
                    album = WriteName(Constants.typeName.album);

                    if (Storage.Albums.ContainsKey(album) && artist.Albums.Find(x => x.Name == album) != null)
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        var songsToAdd = WriteSongs();

                        foreach (var song in songsToAdd)
                        {
                            artist.AddSongsToAlbum(song, album);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no album with this name!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.removeSongsFromAlbum:
                    Console.WriteLine("Enter album from which you want to remove songs: ");
                    album = Console.ReadLine();

                    if (Storage.Albums.ContainsKey(album) && artist.Albums.Find(x => x.Name == album) != null)
                    {
                        Console.WriteLine("Write the songs that you want to be removed, seperated by ', ' :");
                        var songsToRemove = WriteSongs();

                        foreach (var song in songsToRemove)
                        {

                            artist.RemoveSongsFromAlbum(song, album);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no album with this name!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.artistMenu.logout:
                    Console.WriteLine("Logged out...");
                    file.WriteToFile(Storage.GenerateData());
                    break;

                default:
                    LoginDisplay();
                    break;
            }
        }

        public static void PerformListenerActions()
        {
            Listener listener = Storage.Listeners[username];
            ShowListenerDisplay();
            isValidInput(Constants.typeDisplay.listenerDisplay);
            switch (input)
            {
                case (int)Constants.listenerMenu.printInfoAboutMe:
                    Console.WriteLine(listener.ToString());
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.printMyPlaylists:
                    listener.PrintCollection(Constants.typeCollection.playlists);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.printPlaylistInfo:
                    var playlistName = WriteName(Constants.typeName.playlist);
                    listener.PrintCollectionInfo(playlistName);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.printMyFavoriteSongs:
                    listener.PrintCollection(Constants.typeCollection.favorites);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.createPlaylist:
                    playlistName = WriteName(Constants.typeName.playlist);
                    var playlistGengres = WriteGenres();

                    if (listener.PlayLists.Find(x => x.Name == playlistName) == null)
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        var playlistSongs = WriteSongs();

                        PlayList newPlaylist = listener.CreatePlayList(playlistName, playlistGengres, playlistSongs);
                        SavePlaylist(listener, newPlaylist);
                    }
                    else
                    {
                        Console.WriteLine("Playlist already exists!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.removePlaylist:
                    playlistName = WriteName(Constants.typeName.playlist);

                    if (listener.PlayLists.Find(playlist => playlist.Name == playlistName) == null)
                    {
                        Console.WriteLine("Playlist with this name does not exist!");
                    }
                    else
                    {
                        DeletePlaylist(listener, playlistName);
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.addSongsToPlaylist:
                    playlistName = WriteName(Constants.typeName.playlist);

                    if (Storage.Playlists.ContainsKey(playlistName) && listener.PlayLists.Find(x => x.Name == playlistName) != null)
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        var songsToAdd = WriteSongs();

                        foreach (var song in songsToAdd)
                        {
                            listener.AddSongsToPlaylist(song, playlistName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no playlist with this name!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.removeSongsFromPlaylist:
                    playlistName = WriteName(Constants.typeName.playlist);

                    if (Storage.Playlists.ContainsKey(playlistName) && listener.PlayLists.Find(x => x.Name == playlistName) != null)
                    {
                        Console.WriteLine("Write the songs that you want to be removed, seperated by ', ' :");
                        var songsToRemove = WriteSongs();

                        foreach (var song in songsToRemove)
                        {
                            listener.RemoveSongsFromPlaylist(song, playlistName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no playlist with this name!");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.addSongsToFavorites:
                    Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                    var songsToFavorites = WriteSongs();

                    foreach (var song in songsToFavorites)
                    {

                        listener.AddSongsToFavorites(song);
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.removeSongsFromFavorites:
                    Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                    string[] songsToDelete = Console.ReadLine().Split(", ");

                    foreach (string song in songsToDelete)
                    {
                        listener.RemoveSongsFromFavorites(song);
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;

                case (int)Constants.listenerMenu.logout:
                    Console.WriteLine("Logged out...");
                    file.WriteToFile(Storage.GenerateData());
                    break;

                default:
                    Console.WriteLine(Constants.WRONG_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
            }
        }

        public static void ShowArtistDisplay()
        {
            Console.WriteLine("Here are your options: ");
            Console.WriteLine("[1] Print info about me");//[x]
            Console.WriteLine("[2] Print all my albums");//[x]
            Console.WriteLine("[3] Print info about an album");//[x]
            Console.WriteLine("[4] Create album");//[x]
            Console.WriteLine("[5] Remove album");//[x]
            Console.WriteLine("[6] Add songs to album");//[x]
            Console.WriteLine("[7] Remove songs from album");//[x]
            Console.WriteLine("[8] Log Out");//[x]
            Console.Write("Your choise: ");
        }

        public static void ShowListenerDisplay()
        {
            Console.WriteLine("Here are your options: ");
            Console.WriteLine("[1] Print info about me");//[x]
            Console.WriteLine("[2] Print all my playlists");//[x]
            Console.WriteLine("[3] Print info about a playlist");//[x]
            Console.WriteLine("[4] Print my favourite songs");//[x]
            Console.WriteLine("[5] Create a playlist");//[x]
            Console.WriteLine("[6] Remove a playlist");//[x]
            Console.WriteLine("[7] Add songs to a playlist ");//[x]
            Console.WriteLine("[8] Remove songs from a playlist");//[x]
            Console.WriteLine("[9] Add songs to favorites");//[x]
            Console.WriteLine("[10] Remove songs from favorites");//[x]
            Console.WriteLine("[11] Log Out");//[x]
            Console.Write("Your choise: ");
        }

        public static void isValidInput(Constants.typeDisplay typeDisplay)
        {
            if (typeDisplay == Constants.typeDisplay.choicesDisplay)
            {
                bool validInput = false;
                try
                {
                    validInput = int.TryParse(Console.ReadLine(), out input);
                    if (validInput && (input < 1 || input > 2))
                    {
                        validInput = false;
                    }
                }
                catch
                {
                    throw new FormatException(Constants.WRONG_COMMAND_MESSAGE);
                }
            }

            if (typeDisplay == Constants.typeDisplay.artistDisplay)
            {
                bool validInput = false;
                try
                {
                    validInput = int.TryParse(Console.ReadLine(), out input);
                    if (validInput && (input < 1 || input > 8))
                    {
                        validInput = false;
                    }
                }
                catch
                {
                    throw new FormatException(Constants.WRONG_COMMAND_MESSAGE);
                }
            }

            if (typeDisplay == Constants.typeDisplay.listenerDisplay)
            {
                bool validInput = false;
                try
                {
                    validInput = int.TryParse(Console.ReadLine(), out input);
                    if (validInput && (input < 1 || input > 11))
                    {
                        validInput = false;
                    }
                }
                catch
                {
                    throw new FormatException(Constants.WRONG_COMMAND_MESSAGE);
                }
            }
        }

        public static List<Song> WriteSongs()
        {
            string[] songsToAdd = Console.ReadLine().Split(", ");
            List<Song> songsToReturn = new List<Song>();

            foreach (string song in songsToAdd)
            {
                if (Storage.Songs.ContainsKey(song) && song != "")
                {
                    songsToReturn.Add(Storage.Songs[song]);
                }
                else
                {
                    Console.WriteLine("Song as {0} does not exists!", song);
                }
            }

            return songsToReturn;
        }

        public static List<string> WriteGenres()
        {
            Console.WriteLine("Enter gengre/genres separated by ', ': ");
            List<string> albumGenres = Console.ReadLine().Split(", ").ToList();

            return albumGenres;
        }

        public static string WriteYear()
        {
            Console.WriteLine("Enter Year of Release: ");
            string albumYear = Console.ReadLine();
            return albumYear;
        }

        public static string WriteName(Constants.typeName type)
        {
            string name = string.Empty;
            if (type == Constants.typeName.album)
            {
                Console.WriteLine("Enter album name: ");
                name = Console.ReadLine();
            }

            if (type == Constants.typeName.playlist)
            {
                Console.WriteLine("Enter playlist name: ");
                name = Console.ReadLine();
            }

            return name;
        }

        public static void SaveAlbum(Artist artist, Album album)
        {
            if (album != null && !Storage.Albums.ContainsKey(album.Name) && album.Songs.Count > 0)
            {
                Storage.Albums.Add(album.Name, album);
                artist.Albums.Add(album);

                Console.WriteLine("Album has been added successfully!");
            }
            else
            {
                Console.WriteLine("There was an error with creating the album");
            }
        }

        public static void SavePlaylist(Listener listener, PlayList playlist)
        {
            if (playlist != null && !Storage.Playlists.ContainsKey(playlist.Name) && playlist.Songs.Count > 0)
            {
                Storage.Playlists.Add(playlist.Name, playlist);
                listener.PlayLists.Add(playlist);

                Console.WriteLine("Playlist has been added successfully!");
            }
            else
            {
                Console.WriteLine("There was an error with creating the playlist");
            }
        }

        public static void DeletePlaylist(Listener listener, string playlist)
        {
            listener.DeletePlayList(playlist);
            Storage.Playlists.Remove(playlist);
            Console.WriteLine("Playlist has been removed succesfully");
        }

        public static void DeleteAlbum(Artist artist, string album)
        {
            artist.DeleteAlbum(album);
            Storage.Albums.Remove(album);
            Console.WriteLine("Album has been removed succesfully");
        }
    }
}
