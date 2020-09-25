using System.IO;
namespace DaveyVanTilburgWebsite.Models
{
    public class MyThoughtsListItem
    {
        private readonly string _fileName;
        public MyThoughtsListItem(string fileName)
        {
            _fileName = fileName;
        }

        public string Name() => _fileName;
        public string Date() => new FileInfo($"./Posts/{_fileName}.html").CreationTimeUtc.ToString("yyyy-MM-dd");
    }
}