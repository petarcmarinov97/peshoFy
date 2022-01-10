namespace PeshoFy.Classes
{
    class Constants
    {
        public const string FILE_PATH = "C:\\Git Repos\\peshoFy\\PeshoFy\\PeshoFy\\DataBase\\DataBase.txt";
        public const string ARTIST = "artist";
        public const string LISTENER = "listener";
        public const string WELCOME_MESSAGE = "Welcome to the our Console Application\nChoose one of the following options below\n";
        public const string OPTIONS_MESSAGE = "[1] login\n[2] register\nInput your choice: ";
        public const string WRONG_COMMAND_MESSAGE = "Wrong command, try again !\n";
        public const string WAITING_NEXT_COMMAND_MESSAGE = "\nWaiting for the next command...\n";
        
        public enum typeName
        {
            album = 1,
            playlist= 2,
        }
        public enum typeCollection
        {
            playlists = 1,
            favorites = 2,
            albums = 3
        }

        public enum acceptOptions
        {
            login = 1,
            register = 2
        }
        public enum artistMenu
        {
            printInfoAboutMe = 1,
            printMyAlbums = 2,
            printAlbumInfo = 3,
            createAlbum = 4,
            removeAlbum = 5,
            addSongsToAlbum = 6,
            removeSongsFromAlbum = 7,
            logout = 8
        }

        public enum listenerMenu
        {
            printInfoAboutMe = 1,
            printMyPlaylists = 2,
            printPlaylistInfo = 3,
            printMyFavoriteSongs = 4,
            createPlaylist = 5,
            removePlaylist = 6,
            addSongsToPlaylist = 7,
            removeSongsFromPlaylist = 8,
            addSongsToFavorites = 9,
            removeSongsFromFavorites = 10,
            logout = 11
        }

        public enum typeDisplay
        {
            choicesDisplay = 1,
            artistDisplay = 2,
            listenerDisplay = 3
        }
    }
}