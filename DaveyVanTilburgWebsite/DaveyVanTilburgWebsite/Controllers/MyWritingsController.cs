using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class MyWritingsController : Controller
    {
        public IActionResult Index()
            => View(new MyWritingsList());

        public IActionResult ViewItem(string name)
            => View(new MyWritingsPost(name));
    }
}