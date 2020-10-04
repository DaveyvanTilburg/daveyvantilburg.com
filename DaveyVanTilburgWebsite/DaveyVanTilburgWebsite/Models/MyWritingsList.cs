using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyWritingsList
    {
        private readonly string _path;
        private readonly string _searchPattern;
        public MyWritingsList()
        {
            _path = "./Writings";
            _searchPattern = "*.html";
        }

        public IEnumerable<MyWritingsListItem> Items()
            => Directory.GetFiles(_path, _searchPattern)
                .Select(Path.GetFileNameWithoutExtension)
                .Select(n => new MyWritingsListItem(n));
    }
}