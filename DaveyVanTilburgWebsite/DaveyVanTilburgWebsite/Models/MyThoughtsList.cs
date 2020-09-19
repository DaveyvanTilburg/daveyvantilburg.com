using System.Collections.Generic;
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

        public IEnumerable<MyThoughtsListItem> Posts()
            => Directory.GetFiles(_path, _searchPattern)
                .Select(Path.GetFileNameWithoutExtension)
                .Select(n => new MyThoughtsListItem(n));
    }
}