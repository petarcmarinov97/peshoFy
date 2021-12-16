using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    class Storage
    {
        Dictionary<string, string> userTypes = new Dictionary<string, string>();
        Dictionary<string, Listener> listeners = new Dictionary<string, Listener>();
        Dictionary<string, Artist> artists = new Dictionary<string, Artist>();
        public Storage()
        {
        }
        public Dictionary<string, string> UserTypes { get => userTypes; set => userTypes = value; }
        internal Dictionary<string, Listener> Listeners { get => listeners; set => listeners = value; }
        internal Dictionary<string, Artist> Artists { get => artists; set => artists = value; }

        public string ReturnTypeAccount(string username)
        {
            return UserTypes[username];
        }
        public void PrintInfo(string username) //Works need to make it better in printing results
        {
            if (UserTypes[username] == "listener")
            {
                var listener = Listeners[username];
                Console.WriteLine(listener.ToString());
            }

            if(UserTypes[username] == "artist")
            {
                var artist = Artists[username];
                Console.WriteLine(artist.ToString());
            }
        }
    }
}
