using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class Scribbling
    {
        private readonly string _data;
        public Scribbling(string data)
        {
            _data = data;
        }

        public string Date() => GetSection(0);
        public string Type() => GetSection(1);
        public string Title() => GetSection(2);

        private string GetSection(int position)
        {
            try
            {
                return _data.Split(';')[position];
            }
            catch (Exception ex)
            {
                throw new Exception(_data, ex);
            }
        }
    }
}