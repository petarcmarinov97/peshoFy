using NUnit.Framework;
using PeshoFy.Classes;
using PeshoFy;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Tests
{
    internal class Listener_Tests
    {
        private List<PlayList> _sutPlaylists;
        private List<Song> _sutFavoriteSongs;
        private PlayList _sutPlaylist;
        private List<string> _sutPlaylistGenres;
        private Song _sutFirstSong;
        private Song _sutSecondSong;
        private Song _sutTrirdSong;
        private List<string> _sutGenres;
        private List<Song> _sutSongs;
        private Listener _sutListener;

        [SetUp]
        public void Setup()
        {
            _sutPlaylists = new List<PlayList>();
            _sutPlaylistGenres = new List<string>();
            _sutPlaylist = new PlayList("Obseben");
            _sutFirstSong = new Song("first", "04:15");
            _sutSecondSong = new Song("second", "03:45");
            _sutTrirdSong = new Song("trird", "05:15");
            _sutFavoriteSongs = new List<Song>();
            _sutGenres = new List<string>();
            _sutSongs = new List<Song>();
            _sutListener = new Listener("petarcm97", "123", "Petar Marinov", "14/10/1997", _sutGenres, _sutFavoriteSongs, _sutPlaylists);
        }

        [Test]
        public void ToString_Successfully()
        {
            _sutListener.Genres.Add("chalga");
            _sutListener.PlayLists.Add(_sutPlaylist);
            //_sutFavoriteSongs.Add(_sutFirstSong);
            _sutListener.FavoriteSongs.Add(_sutFirstSong);

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   1. chalga\n");
            expected.Append("Playlists: \n   1. Obseben\n");
            expected.Append("Favorites Songs: \n   1. first\n");

            var result = _sutListener.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void ToString_Successfully_Without_Playlists()
        {
            _sutListener.Genres.Add("chalga");
            _sutListener.FavoriteSongs.Add(_sutFirstSong);

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   1. chalga\n");
            expected.Append("Playlists: \n   There are no playlists.\n");
            expected.Append("Favorites Songs: \n   1. first\n");

            var result = _sutListener.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void ToString_Successfully_Without_Favorites()
        {
            _sutListener.Genres.Add("chalga");
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   1. chalga\n");
            expected.Append("Playlists: \n   1. Obseben\n");
            expected.Append("Favorites Songs: \n   There are no favorite songs.\n");

            var result = _sutListener.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void PrintCollection_Successfully_Playlist()
        {
            _sutPlaylist.AddSong(_sutFirstSong);
            _sutPlaylist.AddSong(_sutSecondSong);
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("1. Playlist - Obseben\n   There are 2 songs in this Playlist\n");

            var results = _sutListener.PrintCollection(Constants.typeCollection.playlists);
            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollection_Successfully_Favorites()
        {
            _sutListener.FavoriteSongs.Add(_sutFirstSong);
            _sutListener.FavoriteSongs.Add(_sutSecondSong);

            StringBuilder expected = new StringBuilder();
            expected.Append("   1. first\n   2. second\n");

            var results = _sutListener.PrintCollection(Constants.typeCollection.favorites);
            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully()
        {
            _sutPlaylist.AddSong(_sutFirstSong);
            _sutPlaylist.AddSong(_sutSecondSong);
            _sutPlaylistGenres.Add("rock");
            _sutPlaylist.Genres = _sutPlaylistGenres;
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("Playlist name: Obseben\n");
            expected.Append("Genres: rock \n");
            expected.Append("Songs in the Playlist:\n   1. first\n   2. second\n");
            expected.Append("Playlist Duration: 00:08:00\n");

            var results = _sutListener.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully_Without_Songs()
        {
            _sutPlaylistGenres.Add("rock");
            _sutPlaylist.Genres = _sutPlaylistGenres;
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("Playlist name: Obseben\n");
            expected.Append("Genres: rock \n");
            expected.Append("There are no songs in the Playlist!\n");

            var results = _sutListener.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully_Without_Genres()
        {
            _sutPlaylistGenres.Add("rock");
            _sutListener.Genres = _sutPlaylistGenres;
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("Playlist name: Obseben\n");
            expected.Append("Genres: do not have genres\n");
            expected.Append("There are no songs in the Playlist!\n");

            var results = _sutListener.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }
        
        [Test]
        public void PrintCollectionInfo_Successfully_Wrong_Album_Name()
        {
            _sutPlaylistGenres.Add("rock");
            _sutPlaylist.Genres = _sutPlaylistGenres;
            _sutListener.PlayLists.Add(_sutPlaylist);

            StringBuilder expected = new StringBuilder();
            expected.Append("There are no Playlists with this name!\n");

            var results = _sutListener.PrintCollectionInfo("Yarost");

            Assert.AreEqual(expected.ToString(), results);
        }
    
        [Test]
        public void CreatePlaylist_Successfully()
        {
            var expected = _sutPlaylist;

            var result = _sutListener.CreatePlayList("Obseben", _sutPlaylistGenres, _sutSongs);

            Assert.AreEqual(expected.Name, result.Name);
        }

        [Test]
        public void DeletePlaylist_Successfully()
        {
            _sutListener.PlayLists.Add(_sutPlaylist);

            _sutListener.DeletePlayList("Obseben");

            Assert.AreEqual(_sutListener.PlayLists.Count, 0);
        }

        [Test]
        public void DeletePlaylist_Fail()
        {
            _sutListener.PlayLists.Add(_sutPlaylist);

            _sutListener.DeletePlayList("Obseben2");

            Assert.AreEqual(_sutListener.PlayLists.Count, 1);
        }

        [Test]
        public void AddSongToPlaylist_Successfully()
        {
            _sutListener.PlayLists.Add(_sutPlaylist);
            _sutListener.AddSongsToPlaylist(_sutFirstSong, "Obseben");

            Assert.AreEqual(_sutListener.PlayLists[0].Songs.Count, 1);
        }

        [Test]
        public void RemoveSongFromAlbum_Successfully()
        {
            _sutListener.PlayLists.Add(_sutPlaylist);
            _sutListener.AddSongsToPlaylist(_sutFirstSong, "Obseben");
            _sutListener.AddSongsToPlaylist(_sutSecondSong, "Obseben");

            _sutListener.RemoveSongsFromPlaylist(_sutFirstSong, "Obseben");

            Assert.AreEqual(_sutListener.PlayLists[0].Songs.Count, 1);
        }

        [Test]
        public void RemoveSongFromAlbum_Fail()
        {
            _sutListener.PlayLists.Add(_sutPlaylist);
            _sutListener.AddSongsToPlaylist(_sutFirstSong, "Obseben");
            _sutListener.AddSongsToPlaylist(_sutSecondSong, "Obseben");

            _sutListener.RemoveSongsFromPlaylist(_sutTrirdSong, "Obseben");

            Assert.AreEqual(_sutListener.PlayLists[0].Songs.Count, 2);
        }

        [Test]
        public void AddSongToFavorites_Successfully()
        {
            _sutListener.AddSongsToFavorites(_sutFirstSong);

            Assert.AreEqual(_sutListener.FavoriteSongs.Count, 1);
        }

        [Test]
        public void RemoveSongFromFavorites_Successfully()
        {
            _sutListener.FavoriteSongs.Add(_sutFirstSong);
            _sutListener.FavoriteSongs.Add(_sutSecondSong);

            _sutListener.RemoveSongsFromFavorites(_sutFirstSong.Name);

            Assert.AreEqual(_sutListener.FavoriteSongs.Count, 1);
        }

        [Test]
        public void RemoveSongFromFavorites_Fail()
        {
            _sutListener.FavoriteSongs.Add(_sutFirstSong);
            _sutListener.FavoriteSongs.Add(_sutSecondSong);

            _sutListener.RemoveSongsFromFavorites(_sutTrirdSong.Name);

            Assert.AreEqual(_sutListener.FavoriteSongs.Count, 2);
        }
    }
}
