using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    internal class Login
    {
        static string filePath = "C:\\Git Repos\\peshoFy\\PeshoFy\\PeshoFy\\Utils\\DataFile.txt";
        static string username = string.Empty;
        static string password = string.Empty;
        static bool HasSuccess = false;
        public static void UserLogin()
        {
            FillLoginForm();
        }

        public static void FillLoginForm()
        {
            Console.WriteLine("Login Form");

            WriteUserName();
            WritePassword();
            ReadUsers(username, password);
            while (HasSuccess == false)
            {
                Console.WriteLine("Wrong username or password. Try again: ");
                WriteUserName();
                WritePassword();
                ReadUsers(username, password);
            }
        }

        public static void WriteUserName()
        {
            Console.Write("Username: ");
            username = Console.ReadLine();

            while (IsValidUsername(username) == false)
            {
                Console.Write("Invalid username. Try with a new one!:\nUsername: ");
                username = Console.ReadLine();
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
        public static void ReadUsers(string username, string password)
        {
            string searchString = $"<user><{username}>({password})";
            using (StreamReader reader = File.OpenText(filePath))
            {
                string line = "";
                HasSuccess = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(searchString))
                    {
                        HasSuccess = true;
                        Console.WriteLine("You have successfully Logged in");
                        break;
                    }
                }
            }
        }
        public static bool IsValidPassword(string password)
        {
            Regex passwordRegex = new Regex("^[a-zA-Z0-9]+$");

            return passwordRegex.IsMatch(username);
        }
        public static bool IsValidUsername(string username)
        {
            Regex usernameRegex = new Regex("^[a-zA-Z0-9]+$");

            return usernameRegex.IsMatch(username);
        }
    }
}
