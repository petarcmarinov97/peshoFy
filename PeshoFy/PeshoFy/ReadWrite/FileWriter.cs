using System.IO;

namespace PeshoFy.Classes
{
    internal class FileWriter
    {
        public void WriteToFile(string input)
        {
            File.WriteAllText(Constants.FILE_PATH, string.Empty);
            using StreamWriter file = new StreamWriter(Constants.FILE_PATH);
            file.Write(input);
        }
    }
}
