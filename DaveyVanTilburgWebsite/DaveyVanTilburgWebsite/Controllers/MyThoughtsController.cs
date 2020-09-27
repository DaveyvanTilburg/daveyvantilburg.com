using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class MyThoughtsController : Controller
    {
        public IActionResult Index()
            => View(new MyThoughtsList());

        public IActionResult ViewItem(string name)
            => View(new MyThoughtsPost(name));
    }
}