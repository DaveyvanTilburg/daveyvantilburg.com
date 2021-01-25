using System;

namespace DaveyVanTilburgWebsite.Models
{
    public class Project
    {
        private readonly string _data;
        
        public Project(string data)
        {
            _data = data;
        }

        public string Icon => GetSection(0);
        public string Title => GetSection(1);
        public string Link => GetSection(2);
        public string Description => GetSection(3);

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