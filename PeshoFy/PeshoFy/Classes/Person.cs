using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Person : IPerson
    {
        private string username;
        private string password;
        private string fullName;
        private DateTime dateOfBirth;
        private List<string> genres;

        public Person(string username, string password, string fullName, DateTime dateOfBirth, List<string> genres)
        {
            this.Username = username;
            this.Password = password;
            this.FullName = fullName;
            this.DateOfBirth = dateOfBirth;
            this.Genres = genres;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public List<string> Genres { get => genres; set => genres = value; }
    }
}
