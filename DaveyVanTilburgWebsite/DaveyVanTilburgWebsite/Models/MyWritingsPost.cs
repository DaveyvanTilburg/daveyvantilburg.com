namespace DaveyVanTilburgWebsite.Models
{
    public class MyWritingsPost
    {
        private readonly string _fileName;
        public MyWritingsPost(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html() => System.IO.File.ReadAllText($"./Writings/{_fileName}.html");
    }
}