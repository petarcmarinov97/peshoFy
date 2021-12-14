using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Artist : User
    {
        private List<Album> albums;
        private Guid userId;
        public Artist(string username, string password, string fullName, DateTime dateOfBirth, List<string> genres, List<Album> albums) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.Albums = albums;
        }
        public List<Album> Albums { get => albums; set => albums = value; }
        public Guid UserId
        {
            get => userId;
            set
            {
                //value = Guid.NewGuid();
                userId = Guid.NewGuid();
            }
        }

        public string GetInfo()
        {
            return string.Join("\n", Username, FullName, DateOfBirth, string.Join(" ", Genres), string.Join(" ", albums));
        }
    }
}
