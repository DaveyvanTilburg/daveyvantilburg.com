using System.IO;
namespace DaveyVanTilburgWebsite.Models
{
    public class MyWritingsListItem
    {
        private readonly string _fileName;
        public MyWritingsListItem(string fileName)
        {
            _fileName = fileName;
        }

        public string Name() => _fileName;
    }
}