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
            typeof(Reservation),
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

        private IEnumerable<object> TestItems(string typeSelection)
        {
            if (typeSelection.ToLower() == "book")
            {
                return new List<Book>
                {
                    new("Book 1", "Some book summary", new DateTime(1991, 9, 11, 12, 30, 32)),
                    new("Book 2", "Some other book summary", new DateTime(2001, 9, 11, 12, 30, 32))
                };
            }

            if (typeSelection.ToLower() == "reservation")
            {
                return new List<Reservation>
                {
                    new Reservation("Some snackbar", new DateTime(2010, 11, 15, 16, 30, 15), "Window"),
                    new Reservation("Some 5 star restaurant", new DateTime(2030, 1, 1, 17, 30, 45), "Backroom")
                };
            }

            if (typeSelection.ToLower() == "customer")
            {
                return new List<Customer>
                {
                    new Customer("Some", "Body", new DateTime(1996, 8, 5, 8, 50, 10)),
                    new Customer("No", "Body", new DateTime(2002, 5, 9, 10, 20, 30))
                };
            }

            throw new Exception();
        }
    }
}
