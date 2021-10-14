using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class EssaysController : Controller
    {
        public IActionResult ViewItem(string name)
            => View(new EssaysDetailView(name));
    }
}