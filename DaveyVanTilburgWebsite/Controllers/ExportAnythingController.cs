using System;
using DaveyVanTilburgWebsite.Views.Projects.ExportAnything;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DaveyVanTilburgWebsite.Controllers
{
    public class ExportAnythingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ExportAnythingController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Projects/ExportAnything/index.cshtml");
        }

        [HttpGet]
        public IActionResult Types()
        {
            return Ok(ExportAnything.SupportedTypes());
        }

        [HttpGet]
        public IActionResult TypeDefinition(string typeSelection)
        {
            if (string.IsNullOrWhiteSpace(typeSelection))
                return StatusCode(500);

            return Ok(JsonConvert.SerializeObject(ExportAnything.TypeDefinition(typeSelection)));
        }

        [HttpPost]
        public IActionResult Export(string typeSelection, string[] columns, string[] aliases, string[] indexes, string exportType)
        {
            try
            {
                (byte[] bytes, string mimeType) = ExportAnything.ToFile(typeSelection, columns, aliases, indexes, exportType);
                return File(bytes, mimeType, $"export.{exportType}");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
