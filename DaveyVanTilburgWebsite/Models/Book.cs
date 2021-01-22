using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class Book
    {
        private readonly string _data;
        private readonly Lazy<bool> _recommended;
        
        public Book(string data)
        {
            _data = data;
            _recommended = new Lazy<bool>(() => bool.Parse(GetSection(1)));
        }

        public string Rating => GetSection(0);
        public bool Recommended => _recommended.Value;
        public string Type => GetSection(2);
        public string Icon => GetSection(3);
        public string Title => GetSection(4);
        public string Author => GetSection(5);
        public string Content => GetSection(6);

        private string GetSection(int position)
        {
            try
            {
                return _data.Split(';')[position];
            }
            catch(Exception ex)
            {
                throw new Exception(_data, ex);
            }
        }
    }
}