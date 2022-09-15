using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Controllers
{
    public class WettkaempfeController : Controller
    {
        public IActionResult Index()
        {
            List<WettkampfViewModel> list = new List<WettkampfViewModel>();

            using (var Context = new snowboardingContext())
            {
                var wettkaempfe = Context.Wettkaempfe.ToList();

                foreach(var wettkampf in wettkaempfe)
                {
                    list.Add(new WettkampfViewModel()
                    {
                        Name = wettkampf.Name,
                        Jahr = wettkampf.Jahr,
                        Berg = wettkampf.Berg,
                    });
                }
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> BergList = new List<SelectListItem>();
            using (var Context = new snowboardingContext())
            {
                var bergDataList = Context.Bergs.ToList();

                foreach (var item in bergDataList)
                {
                    BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Name });
                }
            }
            return View(new WettkampfViewModel() { BergList = BergList });
        }

        [HttpPost]
        public IActionResult Add(WettkampfViewModel snowboarder)
        {
            using (var Context = new snowboardingContext())
            {
                Wettkampf WettkampfDB = new Wettkampf()
                {
                    Name = snowboarder.Name,
                    Sponsor = snowboarder.Sponsor,
                    Jahr = snowboarder.Jahr,
                    Berg = snowboarder.Berg,
                };

                Context.Add(WettkampfDB);
                Context.SaveChanges();
            }
            return RedirectToAction(actionName: "Index");
        }
    }
}