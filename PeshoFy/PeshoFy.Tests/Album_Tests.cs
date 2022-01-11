using NUnit.Framework;
using PeshoFy.Classes;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy
{
    public class Album_Tests
    {
        private Album _sutAlbum;
        private Song _sutFirstSong;
        private Song _sutSecondSong;
        private List<string> _sutGenres;
        private List<Song> _sutSongs;
        private string _sutYearOfRelease;

        [SetUp]
        public void Setup()
        {
            _sutAlbum = new Album("Party");
            _sutFirstSong = new Song("first", "04:15");
            _sutSecondSong = new Song("second", "03:45");
            _sutGenres = new List<string>();
            _sutSongs = new List<Song>();
            _sutYearOfRelease = "1997";

        }

        [Test]
        public void AddSong_Successfully()
        {
            _sutAlbum.AddSong(_sutFirstSong);

            Assert.Contains(_sutFirstSong, _sutAlbum.Songs);
        }

        [Test]
        public void RemoveSong_Successfully()
        {
            //Arrange
            _sutAlbum.AddSong(_sutFirstSong);

            //Act 
            _sutAlbum.RemoveSong(_sutFirstSong);

            //Assert
            Assert.AreEqual(_sutAlbum.Songs.Count, 0);
        }

        [Test]
        public void GetGenres_Successfully()
        {
            //Arrange
            _sutGenres.Add("rock");
            _sutGenres.Add("metal");
            _sutAlbum.Genres = _sutGenres;
            //Act 
            var expectedResult = "Genres: " + "rock " + "metal ";
            var result = _sutAlbum.GetGenresInfo();

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

            _sutAlbum.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);
            _sutAlbum.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);

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

            _sutAlbum.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);
            _sutAlbum.CalcsTime(ref sutData, ref hours, ref minutes, ref seconds);

            Assert.AreEqual(2, hours);
            Assert.AreEqual(8, minutes);
            Assert.AreEqual(30, seconds);
        }

        [Test]
        public void GetDurationResult_Successfully_More_Than_Nine()
        {
            var result = _sutAlbum.GetDurationResult(10, 49, 15);

            string expected = $"Album Duration: {10}:{49}:{15}\n";

            Assert.AreEqual(expected, result);

        }

        [Test]
        public void GetDurationResult_Successfully_Less_Than_Nine()
        {
            var result = _sutAlbum.GetDurationResult(7, 3, 5);

            string expected = $"Album Duration: 0{7}:0{3}:0{5}\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSongsInfo_Successfully_When_Have()
        {
            _sutSongs.Add(_sutFirstSong);
            _sutSongs.Add(_sutSecondSong);
            _sutAlbum.Songs = _sutSongs;

            var result = _sutAlbum.GetSongsInfo();
            string expected = "\nSongs in the album:\n   1." + $" {_sutFirstSong.Name}\n   2." + $" {_sutSecondSong.Name}\nAlbum Duration: 00:08:00\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSongsInfo_Successfully_When_DoNotHave()
        {
            var result = _sutAlbum.GetSongsInfo();
            string expected = "\nThere are no songs in the Album!\n";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInfo_Successfully()
        {
            StringBuilder expected = new StringBuilder();

            _sutSongs.Add(_sutFirstSong);
            _sutSongs.Add(_sutSecondSong);
            _sutAlbum.Songs = _sutSongs;
            _sutGenres.Add("rock");
            _sutGenres.Add("metal");
            _sutAlbum.Genres = _sutGenres;
            _sutAlbum.ReleaseDate = _sutYearOfRelease;

            var result = _sutAlbum.GetInfo();

            expected.Append(string.Format("Album name: {0}\n", _sutAlbum.Name));
            expected.Append(string.Format("Year: {0}\n", _sutYearOfRelease));
            expected.Append("Genres: " + "rock " + "metal ");
            expected.Append("\nSongs in the album:\n   1." + $" {_sutFirstSong.Name}\n   2." + $" {_sutSecondSong.Name}\nAlbum Duration: 00:08:00\n");

            Assert.AreEqual(expected.ToString(), result.ToString());
        }
    }
}