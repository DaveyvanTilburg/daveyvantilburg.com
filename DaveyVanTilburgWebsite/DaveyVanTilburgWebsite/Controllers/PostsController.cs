using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            string[] posts = Directory.GetFiles("./Posts", "*.html")
                .Select(Path.GetFileNameWithoutExtension).ToArray();

            ViewBag.Posts = posts;

            return View();
        }

        public IActionResult PostView(string postName)
        {
            string postHtml = System.IO.File.ReadAllText($"./Posts/{postName}.html");

            ViewBag.PostHtml = postHtml;
            ViewBag.PostName = postName;

            return View();
        }
    }
}