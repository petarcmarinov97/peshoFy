using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{

    internal class Register
    {
        static string filePath = "C:\\Git Repos\\peshoFy\\PeshoFy\\PeshoFy\\Utils\\DataFile.txt";
        static string username = string.Empty;
        static string password = string.Empty;
        static string type = string.Empty;
        static string dateOfBirth = string.Empty;
        static Guid userId;
        static bool HasUser = false;
        public static void UserRegister()
        {
            FillRegisterForm();
        }
        public static void FillRegisterForm()
        {
            Console.WriteLine("Register Form");

            WriteUserName();
            WritePassword();
            WriteDateOfBirth();
            WriteType();
            GenerateUserId();
            Console.WriteLine("You have successfully created an account");
            WriteFile();
            MoveToLogin();
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

            ReadUsers(username);
            while (HasUser == true)
            {
                Console.Write("The following username already exists. Try another one\nUsername:");
                username = Console.ReadLine();
                ReadUsers(username);
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
        public static bool IsValidUsername(string username)
        {
            Regex usernameRegex = new Regex("^[a-zA-Z0-9]+$");

            return usernameRegex.IsMatch(username);
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
        public static bool IsValidType(string username)
        {
            return (username == "listener" || username == "artist");
        }
        public static void GenerateUserId()
        {
            userId = Guid.NewGuid();
        }
        public static void ReadUsers(string username)
        {
            string searchString = $"<user><{username}>";
            using (StreamReader reader = File.OpenText(filePath))
            {
                string line = "";
                HasUser = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(searchString))
                    {
                        HasUser = true;
                        break;
                    }
                }
            }
        }
        public static void WriteFile()
        {
            string user = $"<user><{username}>({password})" + "[" + $"{dateOfBirth}" + "]" + "{" + $"{type}" + "}" + "{" + $"{userId}" + "}" + "</user>";

            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine(user);
                }
            }
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(user);
            }
        }
        public static void MoveToLogin()
        {
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Exit the Application");
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Login.UserLogin();
                    break;
                case 2:
                    Console.WriteLine("Goodbye And Have a Nice Day ;) ");
                    break;
            }
        }
    }
}