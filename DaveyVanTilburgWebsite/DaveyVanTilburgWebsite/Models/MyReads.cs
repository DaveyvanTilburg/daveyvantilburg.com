using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyReads
    {
        private readonly string _path;

        public MyReads()
        {
            _path = "./MyReads/Summary.txt";
        }

        public IEnumerable<MyReadsItem> Items()
            => System.IO.File.ReadLines(_path).Select(r => new MyReadsItem(r));
    }
}