using System.IO;
namespace DaveyVanTilburgWebsite.Models
{
    public class Essay
    {
        private readonly string _fileName;
        public Essay(string fileName)
        {
            _fileName = fileName;
        }

        public string Name() => _fileName;
    }
}