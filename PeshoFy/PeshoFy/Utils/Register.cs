using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PeshoFy.Classes
{
    static class Register
    {
        private static string username = string.Empty;
        private static string fullName = string.Empty;
        private static string password = string.Empty;
        private static string type = string.Empty;
        private static string dateOfBirth = string.Empty;
        private static string genresInput = string.Empty;
        private static bool HasUser = false;

        public static void UserRegister()
        {
            FillRegisterForm();
        }

        public static void FillRegisterForm()
        {
            Console.WriteLine("Register Form");

            WriteUsername();
            WritePassword();
            WriteFullName();
            WriteDateOfBirth();
            WriteType();
            WriteGenres();
            Console.WriteLine("You have successfully created an account");
            SaveData();
            Login.UserLogin();
        }

        public static void WriteUsername()
        {
            Console.Write("Username: ");
            username = Console.ReadLine();

            while (IsValidUsername(username) == false)
            {
                Console.Write("Invalid username. Try with a new one!:\nUsername: ");
                username = Console.ReadLine();
            }

            HasUser = CheckUsers(username);
            while (HasUser == true)
            {
                Console.Write("The following username already exists. Try another one\nUsername:");
                username = Console.ReadLine();
                HasUser = CheckUsers(username);
            }
        }
        
        public static void WritePassword()
        {
            Console.Write("Password: ");
            password = Console.ReadLine();

            while (IsValidPassword(password) == false)
            {
                Console.Write("Invalid password. Try with a new one!:\nPassword: ");
                password = Console.ReadLine();
            }
        }
        
        public static void WriteFullName()
        {
            Console.Write("Full Name: ");
            fullName = Console.ReadLine();

            while (IsValidName(fullName) == false)
            {
                Console.Write("Invalid Full Name input. Try with a new one!:\nFull Name: ");
                fullName = Console.ReadLine();
            }
        }
        
        public static void WriteDateOfBirth()
        {
            Console.Write("Date of Birth (DD/MM/YYYY): ");
            dateOfBirth = Console.ReadLine();

            while (IsValidDateOfBirth(dateOfBirth) == false)
            {
                Console.Write("Invalid Date of Birth. Try with a new one!:\nDate of Birth (DD/MM/YYYY): ");
                dateOfBirth = Console.ReadLine();
            }
        }
        
        public static void WriteType()
        {
            Console.Write("Type: ");
            type = Console.ReadLine();

            while (IsValidType(type) == false)
            {
                Console.Write("Invalid type. Try with a new one!:\nType: ");
                type = Console.ReadLine();
            }
        }
        
        public static void WriteGenres()
        {
            Console.Write("Genres seperate by ', ' : ");
            genresInput = Console.ReadLine();
        }
        
        public static bool IsValidUsername(string username)
        {
            Regex usernameRegex = new Regex("^[a-zA-Z0-9]+$");

            return usernameRegex.IsMatch(username);
        }
        
        public static bool IsValidName(string fullName)
        {
            Regex fullNameRegex = new Regex(@"^[a-zA-Z]+(\s[a-zA-Z]+)+$");
            return fullNameRegex.IsMatch(fullName);
        }
        
        public static bool IsValidPassword(string username)
        {
            Regex passwordRegex = new Regex("^[a-zA-Z0-9]+$");

            return passwordRegex.IsMatch(username);
        }
        
        public static bool IsValidDateOfBirth(string dateOfBirth)
        {
            Regex dateRegex = new Regex("^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$");

            return dateRegex.IsMatch(dateOfBirth);
        }
        
        public static bool IsValidType(string type)
        {
            return (type == "listener" || type == "artist");
        }
        
        public static bool CheckUsers(string username)
        {
            return Storage.UserTypes.Keys.Contains(username);
        }
        
        public static void SaveData()
        {
            Storage.UserTypes.Add(username, type);
            List<string> genres = genresInput.Split(", ").ToList<string>();
            List<Album> albums = new List<Album>();
            List<Song> favoriteSongs = new List<Song>();
            List<PlayList> playLists = new List<PlayList>();
            if (type == Constants.ARTIST)
            {
                Storage.Artists.Add(username, new Artist(username, password, fullName, dateOfBirth, genres, albums));
            }
            if (type == Constants.LISTENER)
            {
                Storage.Listeners.Add(username, new Listener(username, password, fullName, dateOfBirth, genres, favoriteSongs, playLists));
            }
        }
    }
}
