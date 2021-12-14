using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{

    class Register
    {
        static string filePath = "C:\\Git Repos\\peshoFy\\PeshoFy\\PeshoFy\\Utils\\DataFile.txt";
        static string username = string.Empty;
        static string password = string.Empty;
        static string type = string.Empty;
        static Guid userId;
        static bool HasUser = false;
        public void UserRegister()
        {
            FillRegisterForm();
        }
        public static void FillRegisterForm()
        {
            Console.WriteLine("Register Form");

            WriteUserName();
            WritePassword();
            WriteType();
            GenerateUserId();
            Console.WriteLine("You have successfully created an account");
            WriteFile();
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
            string user = $"<user><{username}>({password})" + "{" + $"{type}" + "}" + "{" + $"{userId}" + "}" + "</user>";

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
    }
}