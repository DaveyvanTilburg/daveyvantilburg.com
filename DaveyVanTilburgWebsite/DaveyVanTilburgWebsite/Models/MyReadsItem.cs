using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyReadsItem
    {
        private readonly string _data;

        public MyReadsItem(string data)
        {
            _data = data;
        }

        public string Type => GetSection(0);
        public string Icon => GetSection(1);
        public string Title => GetSection(2);
        public string Author => GetSection(3);
        public string Content => GetSection(4);

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