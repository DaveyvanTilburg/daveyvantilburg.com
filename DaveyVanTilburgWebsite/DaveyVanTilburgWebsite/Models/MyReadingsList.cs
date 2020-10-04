using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyReadingsList
    {
        private readonly string _path;

        public MyReadingsList()
        {
            _path = "./MyReadings/Summary.txt";
        }

        public IEnumerable<MyReadingsListItem> Items()
            => System.IO.File.ReadLines(_path).Select(r => new MyReadingsListItem(r));
    }
}