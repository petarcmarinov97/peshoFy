﻿using System;
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
        public static void DisplayMenu()
        {
            DisplayHeader();
            DisplayChoices();
            Displayer();
        }
        public static void DisplayHeader()
        {
            welcome = "Welcome to the our Console Application\nChoose one of the following options below\n";
            options = "[1] login\n[2] register";

            Console.WriteLine(welcome);
            Console.WriteLine(options);
            Console.Write("Input your choice: ");
            input = int.Parse(Console.ReadLine());
        }
        public static void DisplayChoices()
        {
            switch (input)
            {
                case 1:
                    Login.UserLogin();

                    break;
                case 2:
                    Register.UserRegister();
                    break;
                default:
                    break;
            }
        }
        public static void Displayer()
        {
            string[] userInfo = Login.GetAccountType();
            username = userInfo[0];
            accountType = userInfo[1];
            LoginDisplay();
        }
        public static void LoginDisplay()
        {
            int input;

            if (accountType == Constants.ARTIST)
            {
                Artist artist = Storage.Artists[username];
                ArtistDisplay();
                input = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (input)
                {
                    case 1:
                        Console.WriteLine(artist.ToString());
                        Console.WriteLine("Waiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 2:
                        artist.PrintMyAlbums(username);
                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 3:
                        Console.WriteLine("Enter album name: ");
                        string album = Console.ReadLine();
                        artist.AlbumInfo(album);
                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 4:
                        Console.WriteLine("Enter album name: ");
                        album = Console.ReadLine();
                        Console.WriteLine("Enter Year of Release: ");
                        string albumYear = Console.ReadLine();
                        Console.WriteLine("Enter gengre/genres separated by ', ': ");
                        List<string> albumGenres = Console.ReadLine().Split(", ").ToList();
                        Album newAlbum = artist.CreateAlbum(album, albumGenres, albumYear, username);

                        if (newAlbum != null && !Storage.Albums.ContainsKey(album))
                        {
                            Storage.Albums.Add(album, newAlbum);
                            artist.Albums.Add(newAlbum);
                            Console.WriteLine("Album has been added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("There was an error with creating the album");
                        }

                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 5:
                        Console.WriteLine("Enter album name which you want to be removed: ");
                        album = Console.ReadLine();
                        artist.DeleteAlbum(album);
                        Storage.Albums.Remove(album);
                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 6:
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

                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 7:
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

                        Console.WriteLine("Waiting for the next command...");
                        LoginDisplay();
                        break;
                    case 8:
                        break;
                }
            }
            else if (accountType == Constants.LISTENER)
            {
                Listener listener = Storage.Listeners[username];
                ListenerDisplay();
                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Console.WriteLine(listener.ToString());
                        Console.WriteLine("Waiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 2:
                        listener.PrintMyPlayLists(username);
                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                    case 3:
                        Console.WriteLine("Enter album name: ");
                        string playlist = Console.ReadLine();
                        listener.PlaylistInfo(playlist);
                        Console.WriteLine("\nWaiting for the next command...\n");
                        LoginDisplay();
                        break;
                }
            }

        }
        public static void ArtistDisplay()
        {
            Console.WriteLine("Here are your options: ");
            Console.WriteLine("[1] Print info about me");//[x]
            Console.WriteLine("[2] Print all my albums");//[x]
            Console.WriteLine("[3] Print info about an album");//[x]
            Console.WriteLine("[4] Create album");//[x]
            Console.WriteLine("[5] Remove album");//[x]
            Console.WriteLine("[6] Add songs to a album");//[x]
            Console.WriteLine("[7] Remove songs from an album");//[x]
            Console.WriteLine("[8] Log out");//[]
            Console.Write("Your choise: ");
        }
        public static void ListenerDisplay()
        {
            Console.WriteLine("Here are your options: ");
            Console.WriteLine("[1] Print info about me");//[x]
            Console.WriteLine("[2] Print all my playlists");//[]
            Console.WriteLine("[3] Print info about a playlist");//[]
            Console.WriteLine("[4] Print my favourite songs");//[]
            Console.WriteLine("[5] Create a playlist");//[]
            Console.WriteLine("[6] Remove a playlist");//[]
            Console.WriteLine("[7] Add songs to a playlist ");//[]
            Console.WriteLine("[8] Remove songs from a playlist");//[]
            Console.WriteLine("[9] Add songs to favourites");//[]
            Console.WriteLine("[10] Remove songs from favourites");//[]
            Console.WriteLine("[11] Log out");//[]
            Console.Write("Your choise: ");
        }
    }
}
