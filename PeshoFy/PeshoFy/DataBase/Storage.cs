using System;
using System.Collections.Generic;
using System.Text;

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

        public static string GenerateArtists()
        {
            StringBuilder sb = new StringBuilder();

            //<listener><Go6koy><123><Georgi D>[17/12/1996](genres: ['rock', 'metal'])(likedSongs: ['Nothing Else Matters', 'Obseben'])(playlists: [])</listener>
            foreach (Listener listener in Listeners.Values)
            {
                sb.Append(String.Format("<listener><{0}><{1}><{2}>[{3}]", listener.Username, listener.Password, listener.FullName, listener.DateOfBirth));
                
                if (listener.Genres.Count == 0)
                {
                    sb.Append("(genres: [])");
                }
                else
                {
                    sb.Append("(genres: [");
                    for (int i = 0; i < listener.Genres.Count; i++)
                    {
                        if (i != listener.Genres.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", listener.Genres[i]));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])", listener.Genres[i]));
                        }
                    }
                }

                if (listener.FavoriteSongs.Count <= 0)
                {
                    sb.Append("(likedSongs: [])(");
                }
                else
                {
                    sb.Append("(likedSongs: [");
                    for (int i = 0; i < listener.FavoriteSongs.Count; i++)
                    {
                        if (i != listener.FavoriteSongs.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", listener.FavoriteSongs[i].Name));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])", listener.FavoriteSongs[i].Name));
                        }
                    }
                }

                if (listener.PlayLists.Count <= 0)
                {
                    sb.Append("(playlists: [])</listener>\n");
                }
                else
                {
                    sb.Append("playlists: [");
                    for (int i = 0; i < listener.PlayLists.Count; i++)
                    {
                        if (i != listener.PlayLists.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", listener.PlayLists[i].Name));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])</listener>\n", listener.PlayLists[i].Name));
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
