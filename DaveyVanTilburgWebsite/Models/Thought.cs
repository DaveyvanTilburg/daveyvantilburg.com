using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class Thought
    {
        private readonly string _data;
        
        public Thought(string data)
        {
            _data = data;
        }

        public string Date => GetSection(0);
        public string Category => GetSection(1);
        public string Content => GetSection(2);

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