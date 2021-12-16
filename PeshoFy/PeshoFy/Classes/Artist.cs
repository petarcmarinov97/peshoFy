using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Artist : User
    {
        private List<Album> albums;
        public Artist(string username, string password, string fullName, string dateOfBirth, List<string> genres, List<Album> albums) : base(username, password, fullName, dateOfBirth, genres)
        {
            this.Albums = albums;
        }
        public List<Album> Albums { get => albums; set => albums = value; }

        public override string ToString()
        {
            return base.ToString() + " " + string.Join(" ", albums);
        }
    }
}
