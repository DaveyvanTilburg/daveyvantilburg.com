using System.Collections;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers
{
    public class CSV : IParse
    {
        public byte[] Parse(List<dynamic> input)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable)input);

            writer.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            byte[] bytes = ms.ToArray();

            return bytes;
        }
    }
}