using System.Collections.Generic;
using System.Linq;

namespace DaveyVanTilburgWebsite.Models
{
    public class ModelList<T>
    {
        private readonly string _path;

        public ModelList()
        {
            _path = $"./Lists/{typeof(T).Name}s.txt";
        }

        public IEnumerable<T> Items()
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(new[] { typeof(string) });

            return System.IO.File.ReadLines(_path).Select(r => constructor.Invoke(new object[] { r })).Cast<T>();
        }
    }
}