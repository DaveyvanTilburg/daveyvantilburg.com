using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class EssaysController : Controller
    {
        public IActionResult Index()
            => View(new Essays());

        public IActionResult ViewItem(string name)
            => View(new EssayView(name));
    }
}