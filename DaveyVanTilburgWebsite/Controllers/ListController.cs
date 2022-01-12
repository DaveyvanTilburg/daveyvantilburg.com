using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Books()
            => View("~/Views/BookList/Index.cshtml", new ModelList<Book>());

        public IActionResult Essays()
            => View("~/Views/Essays/Index.cshtml", new ModelList<Essay>());
    }
}