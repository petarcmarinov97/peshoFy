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

        public static string GenerateListeners()
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
                            sb.Append(String.Format("\'{0}\'])(", listener.FavoriteSongs[i].Name));
                        }
                    }
                }

                if (listener.PlayLists.Count <= 0)
                {
                    sb.Append("playlists: [])</listener>\n");
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
        public static string GenerateArtists()
        {
            StringBuilder sb = new StringBuilder();

            //<artist><S><1><Stanislav Slanev>[12/8/1959](genres: ['pop', 'rock'])(albums: ['Obseben'])</artist>
            foreach (Artist artist in Artists.Values)
            {
                sb.Append(String.Format("<artist><{0}><{1}><{2}>[{3}]", artist.Username, artist.Password, artist.FullName, artist.DateOfBirth));

                if (artist.Genres.Count == 0)
                {
                    sb.Append("(genres: [])");
                }
                else
                {
                    sb.Append("(genres: [");
                    for (int i = 0; i < artist.Genres.Count; i++)
                    {
                        if (i != artist.Genres.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", artist.Genres[i]));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])", artist.Genres[i]));
                        }
                    }
                }

                if (artist.Albums.Count == 0)
                {
                    sb.Append("(albums: [])</artist>\n");
                }
                else
                {
                    sb.Append("(albums: [");
                    for (int i = 0; i < artist.Albums.Count; i++)
                    {
                        if (i != artist.Albums.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", artist.Albums[i].Name));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])</artist>\n", artist.Albums[i].Name));
                        }
                    }
                }
            }

            return sb.ToString();
        }
        public static string GenerateAlbums()
        {
            StringBuilder sb = new StringBuilder();

            //<album><Obseben>[2002](genres: [rock])(songs: ['Yarost', 'Obseben'])</album>
            foreach (Album album in Albums.Values)
            {
                sb.Append(String.Format("<album><{0}>[{1}]", album.Name, album.ReleaseDate));

                if (album.Genres.Count == 0)
                {
                    sb.Append("(genres :[])");
                }
                else
                {
                    sb.Append("(genres: [");
                    for (int i = 0; i < album.Genres.Count; i++)
                    {
                        if (i != album.Genres.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", album.Genres[i]));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])", album.Genres[i]));
                        }
                    }
                }

                if (album.Songs.Count == 0)
                {
                    sb.Append("(songs :[])</album>\n");
                }
                else
                {
                    sb.Append("(songs: [");
                    for (int i = 0; i < album.Songs.Count; i++)
                    {
                        if (i != album.Songs.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", album.Songs[i].Name));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])</album>\n", album.Songs[i].Name));
                        }
                    }
                }
            }

            return sb.ToString();
        }
        public static string GeneratePlaylists()
        {
            StringBuilder sb = new StringBuilder();

            //<playlists><Black Playlist>(songs: ['Yarost', 'Obseben'])</playlists >
            foreach (PlayList playList in Playlists.Values)
            {
                sb.Append(String.Format("<playlists><{0}>", playList.Name));

                if (playList.Songs.Count == 0)
                {
                    sb.Append("(songs :[])</playlists>\n");
                }
                else
                {
                    sb.Append("(songs: [");
                    for (int i = 0; i < playList.Songs.Count; i++)
                    {
                        if (i != playList.Songs.Count - 1)
                        {
                            sb.Append(String.Format("\'{0}\', ", playList.Songs[i].Name));
                        }
                        else
                        {
                            sb.Append(String.Format("\'{0}\'])</playlists>\n", playList.Songs[i].Name));
                        }
                    }
                }
            }

            return sb.ToString();
        }
        public static string GenerateSongs()
        {
            StringBuilder sb = new StringBuilder();

            //<song><Enter Sandman>[5:31]</song>
            foreach (Song song in Songs.Values)
            {
                sb.Append(String.Format("<song><{0}>", song.Name));

                if (song.Duration == "")
                {
                    sb.Append("[]</song>\n");
                }
                else
                {
                    sb.Append(String.Format("[{0}]</song>\n", song.Duration));
                }
            }

            return sb.ToString();
        }
        public static string GenerateData()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GenerateArtists());
            sb.Append(GenerateListeners());
            sb.Append(GenerateSongs());
            sb.Append(GenerateAlbums());
            sb.Append(GeneratePlaylists());

            return sb.ToString();
        }
    }
}