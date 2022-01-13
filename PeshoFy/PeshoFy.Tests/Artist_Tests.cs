using NUnit.Framework;
using PeshoFy.Classes;
using PeshoFy;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Tests
{
    public class Artist_Tests
    {
        private List<Album> _sutAlbums;
        private Album _sutAlbum;
        private List<string> _sutAlbumGenres;
        private Song _sutFirstSong;
        private Song _sutSecondSong;
        private List<string> _sutGenres;
        private List<Song> _sutSongs;
        private string _sutYearOfRelease;
        private Artist _sutArtist;


        [SetUp]
        public void Setup()
        {
            _sutAlbums = new List<Album>();
            _sutAlbumGenres = new List<string>();
            _sutAlbum = new Album("Obseben");
            _sutFirstSong = new Song("first", "04:15");
            _sutSecondSong = new Song("second", "03:45");
            _sutGenres = new List<string>();
            _sutSongs = new List<Song>();
            _sutYearOfRelease = "1997";
            _sutArtist = new Artist("petarcm97", "123", "Petar Marinov", "14/10/1997", _sutGenres, _sutAlbums);
        }

        [Test]
        public void ToString_Successfully()
        {
            _sutArtist.Genres.Add("chalga");
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   1. chalga\n");
            expected.Append("Albums: \n   1. Obseben\n");

            var result = _sutArtist.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void ToString_Successfully_Without_Album()
        {
            _sutArtist.Genres.Add("chalga");

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   1. chalga\n");
            expected.Append("Albums: \n   There are no albums.\n");

            var result = _sutArtist.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void ToString_Successfully_Without_Genres()
        {
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("Username: petarcm97\n");
            expected.Append("Full name: Petar Marinov\n");
            expected.Append("Date of Birth: 14/10/1997\n");
            expected.Append("Genres: \n   There are no genres.\n");
            expected.Append("Albums: \n   1. Obseben\n");

            var result = _sutArtist.ToString();

            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void PrintCollection_Successfully()
        {
            _sutAlbum.AddSong(_sutFirstSong);
            _sutAlbum.AddSong(_sutSecondSong);
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("1. Album - Obseben\n   There are 2 songs in this Album\n");

            var results = _sutArtist.PrintCollection(Constants.typeCollection.albums);
            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully()
        {
            _sutAlbum.AddSong(_sutFirstSong);
            _sutAlbum.AddSong(_sutSecondSong);
            _sutAlbum.ReleaseDate = _sutYearOfRelease;
            _sutAlbumGenres.Add("rock");
            _sutAlbum.Genres = _sutAlbumGenres;
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("Album name: Obseben\n");
            expected.Append("Year: 1997\n");
            expected.Append("Genres: rock \n");
            expected.Append("Songs in the album:\n   1. first\n   2. second\n");
            expected.Append("Album Duration: 00:08:00\n");

            var results = _sutArtist.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully_Without_Songs()
        {
            _sutAlbum.ReleaseDate = _sutYearOfRelease;
            _sutAlbumGenres.Add("rock");
            _sutAlbum.Genres = _sutAlbumGenres;
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("Album name: Obseben\n");
            expected.Append("Year: 1997\n");
            expected.Append("Genres: rock \n");
            expected.Append("There are no songs in the Album!\n");

            var results = _sutArtist.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully_Without_Genres()
        {
            _sutAlbum.ReleaseDate = _sutYearOfRelease;
            _sutAlbum.Genres = _sutAlbumGenres;
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("Album name: Obseben\n");
            expected.Append("Year: 1997\n");
            expected.Append("Genres: do not have genres\n");
            expected.Append("There are no songs in the Album!\n");

            var results = _sutArtist.PrintCollectionInfo("Obseben");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void PrintCollectionInfo_Successfully_Wrong_Album_Name()
        {
            _sutAlbum.ReleaseDate = _sutYearOfRelease;
            _sutAlbumGenres.Add("rock");
            _sutAlbum.Genres = _sutAlbumGenres;
            _sutArtist.Albums.Add(_sutAlbum);

            StringBuilder expected = new StringBuilder();
            expected.Append("There are no Albums with this name!\n");

            var results = _sutArtist.PrintCollectionInfo("Yarost");

            Assert.AreEqual(expected.ToString(), results);
        }

        [Test]
        public void CreateAlbum_Successfully()
        {
            var expected = _sutAlbum;

            var result = _sutArtist.CreateAlbum("Obseben", _sutAlbumGenres, "1997", _sutSongs);

            Assert.AreEqual(expected.Name, result.Name);
        }

        [Test]
        public void DeleteAlbum_Successfully()
        {
            _sutArtist.Albums.Add(_sutAlbum);

            _sutArtist.DeleteAlbum("Obseben");

            Assert.AreEqual(_sutArtist.Albums.Count, 0);
        }

        [Test]
        public void DeleteAlbum_Fail()
        {
            _sutArtist.Albums.Add(_sutAlbum);

            _sutArtist.DeleteAlbum("Obseben2");

            Assert.AreEqual(_sutArtist.Albums.Count, 1);
        }
    
        [Test]
        public void AddSongToAlbum_Successfully()
        {
            _sutArtist.Albums.Add(_sutAlbum);
            _sutArtist.AddSongsToAlbum(_sutFirstSong, "Obseben");

            Assert.AreEqual(_sutArtist.Albums[0].Songs.Count,1);
        }

        [Test]
        public void RemoveSongFromAlbum_Successfully()
        {
            _sutArtist.Albums.Add(_sutAlbum);
            _sutArtist.AddSongsToAlbum(_sutFirstSong, "Obseben");
            _sutArtist.AddSongsToAlbum(_sutSecondSong, "Obseben");

            _sutArtist.RemoveSongsFromAlbum(_sutFirstSong, "Obseben");

            Assert.AreEqual(_sutArtist.Albums[0].Songs.Count, 1);
        }
    }
}
