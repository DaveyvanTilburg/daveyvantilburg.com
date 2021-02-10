using System.IO;
namespace DaveyVanTilburgWebsite.Models
{
    public class Scribbling
    {
        private readonly string _fileName;
        public Scribbling(string fileName)
        {
            _fileName = fileName;
        }

        public string Name() => _fileName;
    }
}