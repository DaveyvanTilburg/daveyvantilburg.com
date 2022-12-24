using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ClosedXML.Excel;
using CsvHelper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spire.Xls;
using Workbook = Spire.Xls.Workbook;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ExportAnythingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ExportAnythingController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var propertyNames = 
                typeof(TestClass)
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            ViewBag.JSONObject = JsonConvert.SerializeObject(propertyNames);
            return View("~/Views/Projects/ExportAnything/index.cshtml");
        }

        [HttpPost]
        public IActionResult Export(string[] columns, string exportType)
        {
            var testSource = new List<TestClass>
            {
                new("Manuscript", "This is a test", new DateTime(1991, 9, 11, 12, 30, 32)),
                new("Manuscript", "This is a test", new DateTime(1991, 9, 11, 12, 30, 32))
            };

            var result = new List<dynamic>();

            var properties = testSource
                .GetType()
                .GetGenericArguments()[0]
                .GetProperties()
                .Where(p => columns.Contains(p.Name))
                .ToList();

            foreach (object item in testSource)
            {
                IDictionary<string, object> filteredItem = new ExpandoObject();

                foreach (PropertyInfo propertyInfo in properties)
                    filteredItem[propertyInfo.Name] = propertyInfo.GetValue(item);

                result.Add(filteredItem);
            }

            byte[] bytes;
            string mimeType;
            switch (exportType)
            {
                case "csv":
                    bytes = ToCsv(result);
                    mimeType = "text/csv";
                    break;
                case "json":
                    bytes = ToJson(result);
                    mimeType = "application/javascript";
                    break;
                case "xlsx":
                    bytes = ToExcel(result);
                    mimeType = "application/vnd.ms-excel";
                    break;
                case "pdf":
                    bytes = ToPdf(result);
                    mimeType = "application/pdf";
                    break;
                default:
                    return StatusCode(500);
            }

            return File(bytes, mimeType, $"export.{exportType}");
        }

        private byte[] ToCsv(List<dynamic> input)
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

        private byte[] ToJson(List<dynamic> input)
        {
            string serialized = JsonConvert.SerializeObject(input);
            byte[] bytes = Encoding.ASCII.GetBytes(serialized);
            return bytes;
        }

        private byte[] ToExcel(List<dynamic> input)
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
            byte[] bytes = ms.ToArray();
            return bytes;
        }

        private byte[] ToPdf(List<dynamic> input)
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

        [Serializable]
        private class TestClass
        {
            public TestClass(string title, string body, DateTime creationDate)
            {
                Title = title;
                Body = body;
                CreationDate = creationDate;
            }

            public string Title { get; }
            public string Body { get; }
            public DateTime CreationDate { get; }
        }
    }
}
