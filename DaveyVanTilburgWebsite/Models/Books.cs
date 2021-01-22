using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class Books
    {
        private readonly string _path;

        public Books()
        {
            _path = "./BookList/Summary.txt";
        }

        public IEnumerable<Book> Items()
            => System.IO.File.ReadLines(_path).Select(r => new Book(r));
    }
}