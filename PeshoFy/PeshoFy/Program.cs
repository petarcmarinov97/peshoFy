﻿using System;
using System.Collections.Generic;
using PeshoFy.Classes;

namespace PeshoFy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string welcome = "Welcome to the our Console Application\nChoose one of the following options below\n";
            Console.WriteLine(welcome);
            string options = "[1] login\n[2] register";
            Console.WriteLine(options);
            Console.Write("Input your choice: ");
            int input = int.Parse(Console.ReadLine());

            if (input == 2)
            {
                Register register = new Register();
                register.UserRegister();
               
            }
            /*//Class Artist Testing
            List<PlayList> playLists = new List<PlayList>();
            List<Album> albums = new List<Album>();
            List<Song> songs = new List<Song>();
            List<string> genres = new List<string>();
            
            genres.Add("rap");
            genres.Add("pop");
            genres.Add("metal");

            Artist artist = new Artist("Petar", "123456", "Petar Marinov", DateTime.Now, genres, albums);
            //Console.WriteLine(artist.GetInfo());

            //Class Listener Testing
            Album album = new Album("first", "10:15", songs, artist.UserId, "rap", DateTime.Now);
            Song firstSong = new Song("aze", "00:05:15", album.Name, artist.UserId, "rap", DateTime.Now);
            Song secondSong = new Song("bah", "00:07:15", album.Name, artist.UserId, "rap", DateTime.Now);
            Song trirdSong = new Song("cah", "00:03:15", album.Name, artist.UserId, "rap", DateTime.Now);
            songs.Add(firstSong);
            songs.Add(secondSong);
            songs.Add(trirdSong);

            PlayList firstPlayList = new PlayList("Samo Rap", "1:30:55", songs);
            PlayList secondPlayList = new PlayList("Samo Rap Dve", "1:30:55", songs);
            playLists.Add(firstPlayList);
            playLists.Add(secondPlayList);

            Listener listener = new Listener("Ivan", "123456", "Ivan Draganov", DateTime.Now, genres, songs, playLists);
            
            Console.WriteLine(listener.GetInfo());*/
        }
    }
}
