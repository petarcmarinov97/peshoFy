using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PeshoFy.Classes
{
    public class User : IUser
    {
        private string username;
        private string password;
        private string fullName;
        private string dateOfBirth;
        private List<string> genres;

        public User(string username, string password, string fullName, string dateOfBirth, List<string> genres)
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
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public List<string> Genres { get => genres; set => genres = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string output = String.Format("Username: {0}\nFull name: {1}\nDate of Birth: {2}\nGenres: \n", Username, FullName, DateOfBirth, Genres);
            sb.Append(output);
            if (Genres.Count == 0)
            {
                sb.Append("   There are no genres.\n");
            }
            else
            {
                int position = 1;
                foreach (string genre in Genres)
                {
                    sb.Append(String.Format("   {0}. {1}\n", position, genre));
                    position++;
                }
            }

            return sb.ToString();
        }
        
        virtual public void PrintCollection(Constants.typeCollection type) { }
        
        virtual public void PrintCollectionInfo(string collectionName) { }
    }
}
