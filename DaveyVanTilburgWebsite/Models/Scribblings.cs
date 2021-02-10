using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class Scribblings
    {
        private readonly string _path;
        private readonly string _searchPattern;
        public Scribblings()
        {
            _path = "./Scribblings";
            _searchPattern = "*.html";
        }

        public IEnumerable<Scribbling> Items()
            => Directory.GetFiles(_path, _searchPattern)
                .Select(Path.GetFileNameWithoutExtension)
                .Select(n => new Scribbling(n));
    }
}