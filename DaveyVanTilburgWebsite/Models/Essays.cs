using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class Essays
    {
        private readonly string _path;
        private readonly string _searchPattern;
        public Essays()
        {
            _path = "./Essays";
            _searchPattern = "*.html";
        }

        public IEnumerable<Essay> Items()
            => Directory.GetFiles(_path, _searchPattern)
                .Select(Path.GetFileNameWithoutExtension)
                .Select(n => new Essay(n));
    }
}