using System.Text.RegularExpressions;

namespace DaveyVanTilburgWebsite.Models
{
    public class EssaysDetailView
    {
        private readonly string _fileName;
        public EssaysDetailView(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html()
        {
            string fileContents = System.IO.File.ReadAllText($"./Essays/{_fileName}.html");

            string strippedBodyStart = Regex.Replace(fileContents, "<!DOC.*<body.*?>", "", RegexOptions.Singleline);
            string result = Regex.Replace(strippedBodyStart, "</body.*>", "", RegexOptions.Singleline);

            return result;
        }
    }
}