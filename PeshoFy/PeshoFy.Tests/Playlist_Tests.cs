using NUnit.Framework;
using PeshoFy.Classes;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Tests
{
    public class Playlist_Tests
    {
        private PlayList _sutPlaylist;
        private Song _sutFirstSong;
        private Song _sutSecondSong;
        private List<string> _sutGenres;
        private List<Song> _sutSongs;
        
        [SetUp]
        public void Setup()
        {
            _sutPlaylist = new PlayList("PartyMix");
            _sutFirstSong = new Song("first", "04:15");
            _sutSecondSong = new Song("second", "03:45");
            _sutGenres = new List<string>();
            _sutSongs = new List<Song>();
        }

        [Test]
        public void AddSong_Successfully()
        {
            _sutPlaylist.AddSong(_sutFirstSong);

            Assert.Contains(_sutFirstSong, _sutPlaylist.Songs);
        }

        [Test]
        public void RemoveSong_Successfully()
        {
            _sutPlaylist.AddSong(_sutFirstSong);

            _sutPlaylist.RemoveSong(_sutFirstSong);

            Assert.AreEqual(_sutPlaylist.Songs.Count, 0);
        }

        [Test]
        public void GetGenres_Successfully()
        {
            //Arrange
            _sutGenres.Add("rock");
            _sutGenres.Add("metal");
            _sutPlaylist.Genres = _sutGenres;
            //Act 
            var expectedResult = "Genres: " + "rock " + "metal ";
            var result = _sutPlaylist.GetGenresInfo();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CalcsTime_Successfully_Without_Hour()
        {
            string[] sutData = { "04", "15" };
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            _sutPlaylist.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);
            _sutPlaylist.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);

            Assert.AreEqual(0, hours);
            Assert.AreEqual(8, minutes);
            Assert.AreEqual(30, seconds);
        }

        [Test]
        public void CalcsTime_Successfully_With_Hour()
        {
            string[] sutData = { "01", "04", "15" };
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            _sutPlaylist.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);
            _sutPlaylist.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);

            Assert.AreEqual(2, hours);
            Assert.AreEqual(8, minutes);
            Assert.AreEqual(30, seconds);
        }

        [Test]
        public void GetDurationResult_Successfully_More_Than_Nine()
        {
            var result = _sutPlaylist.GetDurationResult(10, 49, 15);

            string expected = $"Playlist Duration: {10}:{49}:{15}\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetDurationResult_Successfully_Less_Than_Nine()
        {
            var result = _sutPlaylist.GetDurationResult(7, 3, 5);

            string expected = $"Playlist Duration: 0{7}:0{3}:0{5}\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSongsInfo_Successfully_When_Have()
        {
            _sutSongs.Add(_sutFirstSong);
            _sutSongs.Add(_sutSecondSong);
            _sutPlaylist.Songs = _sutSongs;

            var result = _sutPlaylist.GetSongsInfo();
            string expected = "\nSongs in the Playlist:\n   1." + $" {_sutFirstSong.Name}\n   2." + $" {_sutSecondSong.Name}\nPlaylist Duration: 00:08:00\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSongsInfo_Successfully_When_DoNotHave()
        {
            var result = _sutPlaylist.GetSongsInfo();
            string expected = "\nThere are no songs in the Playlist!\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInfo_Successfully()
        {
            StringBuilder expected = new StringBuilder();

            _sutSongs.Add(_sutFirstSong);
            _sutSongs.Add(_sutSecondSong);
            _sutPlaylist.Songs = _sutSongs;
            _sutGenres.Add("rock");
            _sutGenres.Add("metal");
            _sutPlaylist.Genres = _sutGenres;

            var result = _sutPlaylist.GetInfo();

            expected.Append(string.Format("Playlist name: {0}\n", _sutPlaylist.Name));
            expected.Append("Genres: " + "rock " + "metal ");
            expected.Append("\nSongs in the Playlist:\n   1." + $" {_sutFirstSong.Name}\n   2." + $" {_sutSecondSong.Name}\nPlaylist Duration: 00:08:00\n");

            Assert.AreEqual(expected.ToString(), result.ToString());
        }
    }
}
