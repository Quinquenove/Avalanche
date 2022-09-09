using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

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

            using (var Context = new snowboardingContext())
            {
                var snowboarders = Context.Snowboarders.ToList();

                foreach(var snowboarder in snowboarders)
                {
                    list.Add(new SnowboarderViewModel()
                    {
                        Nachname = snowboarder.Nachname,
                        Vorname = snowboarder.Vorname,
                        Kuenstlername = snowboarder.Kuenstlername,
                        Geburtstag = snowboarder.Geburtstag,
                        Mitgliednummer = snowboarder.Mitgliedsnummer
                    });
                }
            }
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
            return View(new SnowboarderViewModel() { BergList = BergList, Geburtstag= DateTime.Now });
        }

        [HttpPost]
        public IActionResult Add(SnowboarderViewModel snowboarder)
        {
            using (var Context = new snowboardingContext())
            {
                Snowboarder snowboarderDB = new Snowboarder()
                {
                    Nachname = snowboarder.Nachname,
                    Vorname = snowboarder.Vorname,
                    Kuenstlername = snowboarder.Kuenstlername,
                    Geburtstag = snowboarder.Geburtstag,
                    HausBerg = snowboarder.HausBerg,
                    Mitgliedsnummer = snowboarder.Mitgliednummer
                };

                Context.Add(snowboarderDB);
                Context.SaveChanges();
            }
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public IActionResult Detail(string snowboarderID)
        {
            SnowboarderDetailViewModel snowboarderDetail = new SnowboarderDetailViewModel();
            SnowboarderViewModel snowboarder;
            BergViewModel berg;
            using(var Context = new snowboardingContext())
            {
                var snowboarderDB = Context.Snowboarders.Include(x => x.HausBergNavigation).First(x => x.Mitgliedsnummer.Equals(snowboarderID));
                snowboarder = new SnowboarderViewModel()
                {
                    Nachname = snowboarderDB.Nachname,
                    Vorname = snowboarderDB.Vorname,
                    Kuenstlername = snowboarderDB.Kuenstlername,
                    Geburtstag = snowboarderDB.Geburtstag,
                    HausBerg = snowboarderDB.HausBerg,
                    Mitgliednummer = snowboarderDB.Mitgliedsnummer
                };
                berg = new BergViewModel()
                {
                    Gebirge = snowboarderDB.HausBergNavigation.Gebirge,
                    Name = snowboarderDB.HausBergNavigation.Name,
                    Schwierigkeit = snowboarderDB.HausBergNavigation.Schwierigkeit
                };

                snowboarderDetail.snowboarder = snowboarder;
                snowboarderDetail.berg = berg;

            }
            return View(snowboarderDetail);
        }
    }
}
