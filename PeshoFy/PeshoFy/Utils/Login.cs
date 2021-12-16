using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    class Login
    {
        static string username = string.Empty;
        static string password = string.Empty;
        static bool HasSuccess = false;
        static string accountType = string.Empty;
        public static void UserLogin()
        {
            FillLoginForm();
        }
        public static void FillLoginForm()
        {
            Console.WriteLine("Login Form");

            WriteUserName();
            WritePassword();
            HasSuccess = ReadData.LoginCheck(username, password);

            while (HasSuccess == false)
            {
                Console.WriteLine("Wrong username or password. Try again: ");
                WriteUserName();
                WritePassword();

                HasSuccess = ReadData.LoginCheck(username, password);
            }

            Console.WriteLine("You have successfully Logged in");
            Console.WriteLine("_______________________________");

            accountType = ReadData.Storage.ReturnTypeAccount(username);
            if (accountType == "listener")
            {
                ReadData.ReadListener();
            }
            else
            {
                ReadData.ReadArtist();
            }

            ReadData.Storage.PrintInfo(username);
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
        public static bool IsValidPassword(string password)
        {
            Regex passwordRegex = new Regex("^[a-zA-Z0-9]+$");

            return passwordRegex.IsMatch(password);
        }
        public static bool IsValidUsername(string username)
        {
            Regex usernameRegex = new Regex("^[a-zA-Z0-9]+$");

            return usernameRegex.IsMatch(username);
        }
    }
}
