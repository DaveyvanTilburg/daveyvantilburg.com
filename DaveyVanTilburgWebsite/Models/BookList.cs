using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class BookList
    {
        private readonly string _path;

        public BookList()
        {
            _path = "./BookList/Summary.txt";
        }

        public IEnumerable<Book> Items()
            => System.IO.File.ReadLines(_path).Select(r => new Book(r));
    }
}