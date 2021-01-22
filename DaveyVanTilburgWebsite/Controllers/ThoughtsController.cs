using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ThoughtsController : Controller
    {
        public IActionResult Index()
            => View(new Thoughts());
    }
}