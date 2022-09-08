using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avalanche.Data;

namespace Avalanche.Controllers
{
    public class SnowboarderController : Controller
    {
        public IActionResult Index()
        {
            List<SnowboarderViewModel> list = new List<SnowboarderViewModel>();
            list.Add(new SnowboarderViewModel()
            {
                Nachname = "Mustermann",
                Vorname = "Max",
                Kuenstlername = "Test",
                Geburtstag = DateTime.Now,
                Mitgliednummer = "123-Test"
            });
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> BergList = new List<SelectListItem>() { new SelectListItem() { Value = "TestBerg", Text = "TestBerg" } };
            using (var Context = new snowboardingContext())
            {
                var bergDataList = Context.Bergs.ToList();

                foreach (var item in bergDataList)
                {
                    BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Name });
                }
            }
            //ToDo: Daten für Berge holen, damit Dropdown erstellt werden kann
            return View(new SnowboarderViewModel() { BergList = BergList, Geburtstag= DateTime.Now });
        }

        [HttpPost]
        public IActionResult Add(SnowboarderViewModel snowboarder)
        {
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public IActionResult Detail(string snowboarderID)
        {
            return View();
        }
    }
}
