using System.Collections.Generic;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers
{
    public interface IParse
    {
        byte[] Parse(List<dynamic> input);
    }
}
