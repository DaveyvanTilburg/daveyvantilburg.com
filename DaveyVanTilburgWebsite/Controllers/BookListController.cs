using DaveyVanTilburgWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class BookListController : Controller
    {
        public IActionResult Index()
            => View(new BookList());
    }
}