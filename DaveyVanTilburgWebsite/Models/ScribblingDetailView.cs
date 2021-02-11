using System.Text.RegularExpressions;

namespace DaveyVanTilburgWebsite.Models
{
    public class ScribblingDetailView
    {
        private readonly string _fileName;
        public ScribblingDetailView(string fileName)
        {
            _fileName = fileName;
        }

        public string Title() => _fileName;

        public string Html()
        {
            string fileContents = System.IO.File.ReadAllText($"./Scribblings/{_fileName}.html");

            string strippedBodyStart = Regex.Replace(fileContents, "<!DOC.*<body.*?>", "", RegexOptions.Singleline);
            string result = Regex.Replace(strippedBodyStart, "</body.*>", "", RegexOptions.Singleline);

            return result;
        }
    }
}