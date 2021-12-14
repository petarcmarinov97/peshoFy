using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    internal class Song : Content
    {
        private string album;
        private string genre;
        private DateTime releaseDate;
        private List<Guid> usersId;
        private Guid currentId;

        public Song(string name, string duration, string album, Guid userId, string genre, DateTime releaseDate) : base(name, duration)
        {
            this.Album = album;
            this.Genre = genre;
            this.ReleaseDate = releaseDate;
            this.currentId = userId;
        }

        //Записваме default value ако няма подаден параметър
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
        public string Album { get => album; set => album = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }


    }
}
