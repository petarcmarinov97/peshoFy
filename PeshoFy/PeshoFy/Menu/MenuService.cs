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
                    Console.WriteLine("Enter album name: ");
                    string album = Console.ReadLine();
                    artist.PrintCollectionInfo(album);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
                case (int)Constants.artistMenu.createAlbum:
                    Console.WriteLine("Enter album name: ");
                    album = Console.ReadLine();
                    Console.WriteLine("Enter Year of Release: ");
                    string albumYear = Console.ReadLine();
                    Console.WriteLine("Enter gengre/genres separated by ', ': ");
                    List<string> albumGenres = Console.ReadLine().Split(", ").ToList();
                    Album newAlbum = artist.CreateAlbum(album, albumGenres, albumYear);

                    if (newAlbum != null && !Storage.Albums.ContainsKey(album) && newAlbum.Songs.Count > 0)
                    {
                        Storage.Albums.Add(album, newAlbum);
                        artist.Albums.Add(newAlbum);

                        Console.WriteLine("Album has been added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("There was an error with creating the album");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
                case (int)Constants.artistMenu.removeAlbum:
                    Console.WriteLine("Enter album name which you want to be removed: ");
                    album = Console.ReadLine();
                    artist.DeleteAlbum(album);
                    Storage.Albums.Remove(album);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
                case (int)Constants.artistMenu.addSongsToAlbum:
                    Console.WriteLine("Enter album name in which to add the songs: ");
                    album = Console.ReadLine();

                    if (Storage.Albums.ContainsKey(album))
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        string[] songsToAdd = Console.ReadLine().Split(", ");

                        foreach (string song in songsToAdd)
                        {
                            if (Storage.Songs.ContainsKey(song))
                            {
                                artist.AddSongsToAlbum(Storage.Songs[song], album);
                            }
                            else
                            {
                                Console.WriteLine("Song as {0} does not exists!", song);
                            }
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

                    if (Storage.Albums.ContainsKey(album))
                    {
                        Console.WriteLine("Write the songs that you want to be removed, seperated by ', ' :");
                        string[] songsToRemove = Console.ReadLine().Split(", ");

                        foreach (string song in songsToRemove)
                        {
                            if (Storage.Songs.ContainsKey(song))
                            {
                                artist.RemoveSongsFromAlbum(Storage.Songs[song], album);
                            }
                            else
                            {
                                Console.WriteLine("Song as {0} does not exists!", song);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no playlist with this name!");
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
                    Console.WriteLine("Enter playlist name: ");
                    string playlistName = Console.ReadLine();
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
                    Console.WriteLine("Enter playlist name: ");
                    playlistName = Console.ReadLine();
                    Console.WriteLine("Enter gengre/genres separated by ', ': ");
                    List<string> playlistGengres = Console.ReadLine().Split(", ").ToList();
                    PlayList newPlaylist = listener.CreatePlayList(playlistName, playlistGengres);

                    if (newPlaylist != null && !Storage.Albums.ContainsKey(playlistName) && newPlaylist.Songs.Count > 0)
                    {
                        Storage.Playlists.Add(playlistName, newPlaylist);
                        listener.PlayLists.Add(newPlaylist);

                        Console.WriteLine("Playlist has been added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("There was an error with creating the playlist");
                    }

                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
                case (int)Constants.listenerMenu.removePlaylist:
                    Console.WriteLine("Enter album name which you want to be removed: ");
                    playlistName = Console.ReadLine();
                    listener.DeletePlayList(playlistName);
                    Storage.Playlists.Remove(playlistName);
                    Console.WriteLine(Constants.WAITING_NEXT_COMMAND_MESSAGE);
                    LoginDisplay();
                    break;
                case (int)Constants.listenerMenu.addSongsToPlaylist:
                    Console.WriteLine("Enter playlist name in which to add the songs: ");
                    playlistName = Console.ReadLine();

                    if (Storage.Playlists.ContainsKey(playlistName))
                    {
                        Console.WriteLine("Write the songs that you want to be added, separated by ', ' :");
                        string[] songsToAdd = Console.ReadLine().Split(", ");

                        foreach (string song in songsToAdd)
                        {
                            if (Storage.Songs.ContainsKey(song))
                            {
                                listener.AddSongsToPlaylist(Storage.Songs[song], playlistName);
                            }
                            else
                            {
                                Console.WriteLine("Song as {0} does not exists!", song);
                            }
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
                    Console.WriteLine("Enter playlist from which you want to remove songs: ");
                    playlistName = Console.ReadLine();

                    if (Storage.Playlists.ContainsKey(playlistName))
                    {
                        Console.WriteLine("Write the songs that you want to be removed, seperated by ', ' :");
                        string[] songsToRemove = Console.ReadLine().Split(", ");

                        foreach (string song in songsToRemove)
                        {
                            if (Storage.Songs.ContainsKey(song))
                            {
                                listener.RemoveSongsFromPlaylist(Storage.Songs[song], playlistName);
                            }
                            else
                            {
                                Console.WriteLine("Song as {0} does not exists!", song);
                            }
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
                    string[] songsToFavorites = Console.ReadLine().Split(", ");

                    foreach (string song in songsToFavorites)
                    {
                        if (Storage.Songs.ContainsKey(song))
                        {
                            listener.AddSongsToFavorites(Storage.Songs[song]);
                        }
                        else
                        {
                            Console.WriteLine("Song as {0} does not exists!", song);
                        }
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

    }
}
