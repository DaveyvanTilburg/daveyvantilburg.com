using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Books()
            => View("~/Views/BookList/Index.cshtml", new ModelList<Book>());

        public IActionResult Thoughts()
            => View("~/Views/Thoughts/Index.cshtml", new ModelList<Thought>());

        public IActionResult Projects()
            => View("~/Views/Projects/Index.cshtml", new ModelList<Project>());
    }
}