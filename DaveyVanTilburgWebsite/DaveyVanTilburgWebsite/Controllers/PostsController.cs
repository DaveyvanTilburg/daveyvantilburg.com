using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
            => View(new MyThoughtsList());

        public IActionResult PostView(string postName)
            => View(new MyThoughtsPost(postName));
    }
}