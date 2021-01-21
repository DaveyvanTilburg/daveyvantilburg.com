using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class MyReadsController : Controller
    {
        public IActionResult Index()
            => View(new MyReads());
    }
}