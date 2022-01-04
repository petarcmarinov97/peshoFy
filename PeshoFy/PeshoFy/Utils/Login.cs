using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    static class Login
    {
        private static string username = string.Empty;
        private static string password = string.Empty;
        private static bool approved = false;
        private static string accountType = string.Empty;
        public static void UserLogin()
        {
            FillLoginForm();
        }
        public static void FillLoginForm()
        {
            Console.WriteLine("Login Form");

            WriteUserName();
            WritePassword();
            LoginCheck();

            while (approved == false)
            {
                Console.WriteLine("Wrong username or password. Try again: ");
                WriteUserName();
                WritePassword();

                LoginCheck();
            }

            Console.WriteLine("You have successfully Logged in");
            Console.WriteLine("_______________________________");

        }
        public static void LoginCheck()
        {
            if (Storage.UserTypes.Keys.Contains(username))
            {
                accountType = Storage.UserTypes[username];
                if (accountType == Constants.LISTENER)
                {
                    if (Storage.Listeners[username].Password == password)
                    {
                        approved = true;
                    }
                }

                if (accountType == Constants.ARTIST)
                {
                    if (Storage.Artists[username].Password == password)
                    {
                        approved = true;
                    }
                }
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
        public static string[] GetAccountType()
        {
            string[] userInfo = new string[2];
            userInfo[0] = username;
            userInfo[1] = accountType;
            return userInfo;
        }

    }
}
