using System.IO;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyThoughtsList
    {
        private readonly string _path;
        private readonly string _searchPattern;
        public MyThoughtsList()
        {
            _path = "./Posts";
            _searchPattern = "*.html";
        }

        public string[] PostNames()
            => Directory.GetFiles(_path, _searchPattern)
                .Select(Path.GetFileNameWithoutExtension).ToArray();
    }
}