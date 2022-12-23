using System;
using System.Collections;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            //byte[] bytes;
            //string mimeType;
            //switch (exportType)
            //{
            //    case "csv":
            //        bytes = ToCsv(result);
            //        mimeType = "text/csv";
            //        break;
            //    case "json":
            //        bytes = ToJson(result);
            //        mimeType = "application/javascript";
            //        break;
            //    case "xlsx":
            //        bytes = ToExcel(result);
            //        mimeType = "application/vnd.ms-excel";
            //        break;
            //    default:
            //        return StatusCode(500);
            //}
            
            //return File(bytes, mimeType, $"export.{exportType}");

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            worksheet.Cell("A1").Value = "Hello World!";

            using var ms = new MemoryStream();


            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Flush();

                return new FileContentResult(stream.ToArray(),
                       "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                       {
                           FileDownloadName = "XXXName.xlsx"
                       };
            }
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
            worksheet.Cell("A1").Value = "Hello World!";

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);

            ms.Seek(0, SeekOrigin.Begin);
            byte[] bytes = ms.ToArray();

            return bytes;
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
