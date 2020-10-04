using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class MyReadingsListItem
    {
        private readonly string _data;

        public MyReadingsListItem(string data)
        {
            _data = data;
        }

        public string Type => GetSection(0);
        public string Icon => GetSection(1);
        public string Start => GetSection(2);
        public string Finished => GetSection(3);
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