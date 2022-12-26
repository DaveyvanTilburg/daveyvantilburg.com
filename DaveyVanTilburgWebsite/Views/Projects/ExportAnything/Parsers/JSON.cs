using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers
{
    public class JSON : IParse
    {
        public byte[] Parse(List<dynamic> input)
        {
            string serialized = JsonConvert.SerializeObject(input);
            byte[] bytes = Encoding.ASCII.GetBytes(serialized);
            return bytes;
        }
    }
}