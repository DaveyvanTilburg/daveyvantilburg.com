using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class MyReadingsController : Controller
    {
        public IActionResult Index()
            => View(new MyReadingsList());
    }
}