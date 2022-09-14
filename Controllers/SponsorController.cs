using Microsoft.AspNetCore.Mvc;

namespace Avalanche.Controllers
{
    public class SponsorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSponsor()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSponsoring()
        {
            return View();
        }
    }
}
