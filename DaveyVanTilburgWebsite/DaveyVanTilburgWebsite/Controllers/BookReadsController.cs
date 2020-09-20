using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class BookReadsController : Controller
    {
        public IActionResult Index()
            => View(new MyBookReadsList());
    }
}