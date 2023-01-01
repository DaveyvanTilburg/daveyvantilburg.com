using DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes;
using System.Collections.Generic;
using System;
using System.Linq;
using DaveyVanTilburgWebsite.Views.Projects.ExportAnything.Parsers;
using System.Dynamic;
using System.Reflection;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything
{
    public static class ExportAnything
    {
        private record struct OutputMarkup(string column, string alias, int index)
        {
        }

        private static List<Type> _supportedTypes = new() {
            typeof(Book),
            typeof(Customer)
        };

        public static List<string> SupportedTypes()
        {
            return _supportedTypes
                .Select(t => t.Name.ToLower())
                .ToList();
        }

        public static string[] TypeDefinition(string type)
        {
            Type typeSelected = _supportedTypes
                .First(t => string.Equals(t.Name, type, StringComparison.CurrentCultureIgnoreCase));

            var propertyNames = 
                typeSelected
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            return propertyNames;
        }

        public static (byte[] bytes, string mimeType) ToFile(string typeSelection, string[] columns, string[] aliases, string[] indexes, string exportType)
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
                        return (Array.Empty<byte>(), "");
                }

                byte[] bytes = parser.Parse(result);

                return (bytes, mimeType);
        }

        private static IEnumerable<object> TestItems(string typeSelection)
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