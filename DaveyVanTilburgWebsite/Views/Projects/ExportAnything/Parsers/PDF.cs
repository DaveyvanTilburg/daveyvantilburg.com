using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spire.Xls;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers
{
    public class PDF : IParse
    {
        public byte[] Parse(List<dynamic> input)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            
            //HeaderRow
            IDictionary<string, object> headerItem = input[0];
            foreach ((string columnHeader, int index) in headerItem.Keys.Select((key, index) => (key, index)))
                worksheet.Cell(1, index + 1).SetValue(columnHeader);

            //DataRows
            foreach((IDictionary<string, object> entry, int entryIndex) in input.Select((item, index) => ((IDictionary<string, object>)item, index)))
            foreach ((object value, int index) in entry.Values.Select((value, index) => (value, index)))
                worksheet.Cell(entryIndex + 2, index + 1).SetDataType(DataType(value)).SetValue(value?.ToString() ?? string.Empty);

            worksheet.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);

            ms.Seek(0, SeekOrigin.Begin);
            var test = new Workbook();
            test.LoadFromStream(ms);

            using var ms2 = new MemoryStream();
            test.SaveToStream(ms2, FileFormat.PDF);

            byte[] bytes = ms2.ToArray();
            return bytes;
        }

        private XLDataType DataType(object source)
        {
            switch (source)
            {
                case string _:
                    return XLDataType.Text;
                case int _:
                    return XLDataType.Number;
                case bool _:
                    return XLDataType.Boolean;
                case DateTime _:
                    return XLDataType.DateTime;
                case TimeSpan _:
                    return XLDataType.TimeSpan;
                default:
                    return XLDataType.Text;
            }
        }
    }
}
