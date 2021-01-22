using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class Thoughts
    {
        private readonly string _path;

        public Thoughts()
        {
            _path = "./Thoughts/Summary.txt";
        }

        public IEnumerable<Thought> Items()
            => System.IO.File.ReadLines(_path).Select(r => new Thought(r));
    }
}
