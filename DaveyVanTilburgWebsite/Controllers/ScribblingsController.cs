using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ScribblingsController : Controller
    {
        public IActionResult Index()
            => View(new Scribblings());

        public IActionResult ViewItem(string name)
            => View(new ScribblingDetailView(name));
    }
}