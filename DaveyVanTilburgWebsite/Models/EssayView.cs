namespace DaveyVanTilburgWebsite.Models
{
    public class EssayView
    {
        private readonly string _fileName;
        public EssayView(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html() => System.IO.File.ReadAllText($"./Essays/{_fileName}.html");
    }
}