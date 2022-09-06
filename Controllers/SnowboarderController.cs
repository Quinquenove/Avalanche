using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;

namespace Avalanche.Controllers
{
    public class SnowboarderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new SnowboarderViewModel());
        }

        [HttpPost]
        public IActionResult Add(SnowboarderViewModel snowboarder)
        {
            return RedirectToAction(actionName: "Index");
        }
    }
}
