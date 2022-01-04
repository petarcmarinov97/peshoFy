using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoFy.Classes
{
    static class Storage
    {
        static Dictionary<string, string> userTypes = new Dictionary<string, string>();
        static Dictionary<string, Listener> listeners = new Dictionary<string, Listener>();
        static Dictionary<string, Artist> artists = new Dictionary<string, Artist>();
        static Dictionary<string, PlayList> playlists = new Dictionary<string, PlayList>();
        static Dictionary<string, Album> albums = new Dictionary<string, Album>();
        static Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public static Dictionary<string, string> UserTypes { get => userTypes; set => userTypes = value; }
        public static Dictionary<string, Listener> Listeners { get => listeners; set => listeners = value; }
        public static Dictionary<string, Artist> Artists { get => artists; set => artists = value; }
        public static Dictionary<string, PlayList> Playlists { get => playlists; set => playlists = value; }
        public static Dictionary<string, Album> Albums { get => albums; set => albums = value; }
        public static Dictionary<string, Song> Songs { get => songs; set => songs = value; }
    }
}
