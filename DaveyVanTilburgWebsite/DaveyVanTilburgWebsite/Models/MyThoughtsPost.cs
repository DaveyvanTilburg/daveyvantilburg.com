namespace DaveyVanTilburgWebsite.Models
{
    public class MyThoughtsPost
    {
        private readonly string _fileName;
        public MyThoughtsPost(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html() => System.IO.File.ReadAllText($"./Posts/{_fileName}.html");
    }
}