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
                        Mitgliedsnummer = snowboarder.Mitgliedsnummer
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
                    Mitgliedsnummer = snowboarder.Mitgliedsnummer
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
                var snowboarderDB = Context.Snowboarders
                                    .Include(x => x.HausBergNavigation)
                                    .Include(x => x.Profi)
                                    .First(x => x.Mitgliedsnummer.Equals(snowboarderID));
                snowboarder = new SnowboarderViewModel()
                {
                    Nachname = snowboarderDB.Nachname,
                    Vorname = snowboarderDB.Vorname,
                    Kuenstlername = snowboarderDB.Kuenstlername,
                    Geburtstag = snowboarderDB.Geburtstag,
                    HausBerg = snowboarderDB.HausBerg,
                    Mitgliedsnummer = snowboarderDB.Mitgliedsnummer
                };
                berg = new BergViewModel()
                {
                    Gebirge = snowboarderDB.HausBergNavigation.Gebirge,
                    Name = snowboarderDB.HausBergNavigation.Name,
                    Schwierigkeit = snowboarderDB.HausBergNavigation.Schwierigkeit
                };
                var sponsor = new SponsoringViewModel()
                {
                    Snowboarder = snowboarder.Mitgliedsnummer,
                    Sponsor = "TestSponsor",
                    Vertragsart = "TestVertrag"
                };
                if(snowboarderDB.Profi != null)
                {
                    snowboarderDetail.Profi = new ProfiViewModel()
                    {
                        Lizenznummer = snowboarderDB.Profi.Lizenznummer,
                        Weltcuppunkte = snowboarderDB.Profi.Weltcuppunkte,
                        Mitgliedsnummer = snowboarderDB.Mitgliedsnummer,
                        BestTrick = snowboarderDB.Profi.BestTrick
                    };
                }

                snowboarderDetail.Snowboarder = snowboarder;
                snowboarderDetail.Berg = berg;
                snowboarderDetail.Sponsoring = new List<SponsoringViewModel>() { sponsor };

            }
            return View(snowboarderDetail);
        }

        public IActionResult Delete(string snowboarderID)
        {
            using (var Context = new snowboardingContext())
            {
                var snowboarder = Context.Snowboarders.First(x => x.Mitgliedsnummer.Equals(snowboarderID));

                Context.Snowboarders.Remove(snowboarder);

                Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(string snowboarderID)
        {
            SnowboarderViewModel snowboarder;
            Snowboarder snowboarderDB;
            List<SelectListItem> BergList = new List<SelectListItem>();
            using (var Context = new snowboardingContext())
            {
                snowboarderDB = Context.Snowboarders.Include(x => x.HausBergNavigation).First(x => x.Mitgliedsnummer.Equals(snowboarderID));

                var bergDataList = Context.Bergs.ToList();

                foreach (var item in bergDataList)
                {
                    BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Name });
                }
            }

            snowboarder = new SnowboarderViewModel()
            {
                Nachname = snowboarderDB.Nachname,
                Vorname = snowboarderDB.Vorname,
                Kuenstlername = snowboarderDB.Kuenstlername,
                Geburtstag = snowboarderDB.Geburtstag,
                Mitgliedsnummer = snowboarderDB.Mitgliedsnummer,
                HausBerg = snowboarderDB.HausBerg,
                BergList = BergList
            };

            return View(snowboarder);
        }

        [HttpPost]
        public IActionResult Update(SnowboarderViewModel snowboarder)
        {
            Snowboarder snowboarderDB;
            using (var Context = new snowboardingContext())
            {
                snowboarderDB = Context.Snowboarders.First(x => x.Mitgliedsnummer.Equals(snowboarder.Mitgliedsnummer));

                snowboarderDB.Nachname = snowboarder.Nachname;
                snowboarderDB.Vorname = snowboarder.Vorname;
                snowboarderDB.Kuenstlername = snowboarder.Kuenstlername;
                snowboarderDB.Geburtstag = snowboarder.Geburtstag;
                snowboarderDB.HausBerg = snowboarder.HausBerg;

                Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddProfi(string snowboarderID)
        {
            return View(new ProfiViewModel() { Mitgliedsnummer = snowboarderID});
        }

        [HttpPost]
        public IActionResult AddProfi(ProfiViewModel profi)
        {
            using(var Context = new snowboardingContext())
            {
                Profi profiDB = new Profi()
                {
                    Lizenznummer = profi.Lizenznummer,
                    Weltcuppunkte = profi.Weltcuppunkte,
                    Mitgliedsnummer = profi.Mitgliedsnummer,
                    BestTrick = profi.BestTrick
                };

                Context.Add(profiDB);
                Context.SaveChanges();
            }
            return RedirectToAction("Detail", new { snowboarderID = profi.Mitgliedsnummer });
        }
    }
}