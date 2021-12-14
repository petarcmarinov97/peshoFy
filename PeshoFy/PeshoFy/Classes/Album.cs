using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Album : Content, ISongsContainer
    {
        private string genre;
        private List<Guid> usersId;
        private List<Song> songs;
        private Guid currentId;
        private DateTime releaseDate;

        public Album(string name, string duration, List<Song> songs,Guid userId, string genre, DateTime releaseDate) : base(name, duration)
        {
            this.Genre = genre;
            this.releaseDate = releaseDate;
            this.Songs = songs;
            this.currentId = userId;
        }
        public List<Guid> UsersId
        {
            get => usersId;
            set
            {
                value.Add(currentId);
                usersId = value;
            }
        }
        public string Genre { get => genre; set => genre = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public List<Song> Songs
        {
            get => songs;
            set => songs = value.Count > 0 ? value : null;
        }
    }
}
