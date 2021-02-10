namespace DaveyVanTilburgWebsite.Models
{
    public class ScribblingDetailView
    {
        private readonly string _fileName;
        public ScribblingDetailView(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html() => System.IO.File.ReadAllText($"./Scribblings/{_fileName}.html");
    }
}