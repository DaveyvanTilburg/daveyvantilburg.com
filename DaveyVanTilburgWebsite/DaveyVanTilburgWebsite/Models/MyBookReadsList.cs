using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyBookReadsList
    {
        private readonly string _path;

        public MyBookReadsList()
        {
            _path = "./BookReads/BookReads.txt";
        }

        public IEnumerable<MyBookReadsListItem> BookReads()
            => System.IO.File.ReadLines(_path).Select(r => new MyBookReadsListItem(r));
    }
}