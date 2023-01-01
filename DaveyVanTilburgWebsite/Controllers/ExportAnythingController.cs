using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes;
using DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers;
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
            return View("~/Views/Projects/ExportAnything/index.cshtml");
        }

        private record struct OutputMarkup(string column, string alias, int index)
        {
        }

        private static List<Type> SupportedTypes = new() {
            typeof(Book),
            typeof(Customer)
        };

        [HttpGet]
        public IActionResult Types()
        {
            return Ok(
                SupportedTypes
                .Select(t => t.Name.ToLower())
                .ToList()
            );
        }

        [HttpGet]
        public IActionResult TypeDefinition(string typeSelection)
        {
            if (string.IsNullOrWhiteSpace(typeSelection))
                return StatusCode(500);

            Type typeSelected = SupportedTypes
                .First(t => string.Equals(t.Name, typeSelection, StringComparison.CurrentCultureIgnoreCase));

            var propertyNames = 
                typeSelected
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            return Ok(JsonConvert.SerializeObject(propertyNames));
        }

        [HttpPost]
        public IActionResult Export(string typeSelection, string[] columns, string[] aliases, string[] indexes, string exportType)
        {
            try
            {
                List<OutputMarkup> outputMarkups = columns.Select((_, index) => new OutputMarkup(columns[index], aliases[index], int.Parse(indexes[index])))
                .OrderBy(o => o.index)
                .ToList();

                var testSource = TestItems(typeSelection);

                var result = new List<dynamic>();

                var properties = testSource
                    .GetType()
                    .GetGenericArguments()[0]
                    .GetProperties();

                foreach (object item in testSource)
                {
                    IDictionary<string, object> filteredItem = new ExpandoObject();

                    foreach (OutputMarkup outputMarkup in outputMarkups)
                    {
                        PropertyInfo propertyInfo = properties.First(p => p.Name == outputMarkup.column);
                        filteredItem[outputMarkup.alias] = propertyInfo.GetValue(item);
                    }

                    result.Add(filteredItem);
                }

                IParse parser;
                string mimeType;
                switch (exportType)
                {
                    case "csv":
                        parser = new CSV();
                        mimeType = "text/csv";
                        break;
                    case "json":
                        parser = new JSON();
                        mimeType = "application/javascript";
                        break;
                    case "xlsx":
                        parser = new Excel();
                        mimeType = "application/vnd.ms-excel";
                        break;
                    case "pdf":
                        parser = new PDF();
                        mimeType = "application/pdf";
                        break;
                    default:
                        return StatusCode(500);
                }

                byte[] bytes = parser.Parse(result);

                return File(bytes, mimeType, $"export.{exportType}");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private IEnumerable<object> TestItems(string typeSelection)
        {
            if (typeSelection.ToLower() == "book")
            {
                return new List<Book>
                {
                    new("The alchemist", new DateTime(2015, 7, 02), "Paulo Coelho", "Paperback", 224),
                    new("Immune", new DateTime(2021, 11, 02), "Philipp Dettmer", "Hardcover", 368),
                    new("Ordinary men", new DateTime(2001, 6, 28), "Christopher R. Browning", "Paperback", 304),
                    new("Blockchain Basics", new DateTime(2017, 3, 16), "Daniel Drescher", "Paperback", 255)
                };
            }

            if (typeSelection.ToLower() == "customer")
            {
                return new List<Customer>
                {
                    new("Leonardo", "da Vinci", new DateTime(1452, 1, 16), new DateTime(2020, 1, 1), "LeoDaVi@rando.com", new DateTime(2022, 5, 12), 12, 1000.50m),
                    new("Claude", "Monet", new DateTime(1840, 8, 11), new DateTime(1999, 12, 30), "GotThatMonet@gmail.com", new DateTime(2022, 7, 6), 1, 67.96m)
                };
            }

            throw new Exception();
        }
    }
}
