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
                        WettkampfId = wettkampf.WettkampfId
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
            return View(new WettkampfViewModel() { BergList = BergList, Geburtstag= DateTime.Now });
        }

        [HttpPost]
        public IActionResult Add(WettkampfViewModel wettkampf)
        {
            using (var Context = new snowboardingContext())
            {
                Wettkampf wettkampfDB = new Wettkampf()
                {
                    Nachname = wettkampf.Nachname,
                    Vorname = wettkampf.Vorname,
                    Kuenstlername = wettkampf.Kuenstlername,
                    Geburtstag = wettkampf.Geburtstag,
                    HausBerg = wettkampf.HausBerg,
                    Mitgliedsnummer = wettkampf.Mitgliedsnummer
                };

                Context.Add(wettkampfDB);
                Context.SaveChanges();
            }
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public IActionResult Detail(string wettkampfID)
        {
            WettkampfDetailViewModel wettkampfDetail = new WettkampfDetailViewModel();
            WettkampfViewModel wettkampf;
            BergViewModel berg;
            using(var Context = new snowboardingContext())
            {
                var wettkampfDB = Context.Wettkaempfe
                                    .Include(x => x.HausBergNavigation)
                                    .First(x => x.Mitgliedsnummer.Equals(wettkampfID));
                wettkampf = new WettkampfViewModel()
                {
                    Nachname = wettkampfDB.Nachname,
                    Vorname = wettkampfDB.Vorname,
                    Kuenstlername = wettkampfDB.Kuenstlername,
                    Geburtstag = wettkampfDB.Geburtstag,
                    HausBerg = wettkampfDB.HausBerg,
                    Mitgliedsnummer = wettkampfDB.Mitgliedsnummer
                };
                berg = new BergViewModel()
                {
                    Gebirge = wettkampfDB.HausBergNavigation.Gebirge,
                    Name = wettkampfDB.HausBergNavigation.Name,
                    Schwierigkeit = wettkampfDB.HausBergNavigation.Schwierigkeit
                };
                var sponsor = new SponsoringViewModel()
                {
                    Wettkampf = wettkampf.Mitgliedsnummer,
                    Sponsor = "TestSponsor",
                    Vertragsart = "TestVertrag"
                };

                wettkampfDetail.Wettkampf = wettkampf;
                wettkampfDetail.Berg = berg;
                wettkampfDetail.Sponsoring = new List<SponsoringViewModel>() { sponsor };

            }
            return View(wettkampfDetail);
        }

        [HttpGet]
        public IActionResult Update(string wettkampfID)
        {
            WettkampfViewModel wettkampf;
            Wettkampf wettkampfDB;
            List<SelectListItem> BergList = new List<SelectListItem>();
            using (var Context = new snowboardingContext())
            {
                wettkampfDB = Context.Wettkaempfe.Include(x => x.HausBergNavigation).First(x => x.Mitgliedsnummer.Equals(wettkampfID));

                var bergDataList = Context.Bergs.ToList();

                foreach (var item in bergDataList)
                {
                    BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Name });
                }
            }

            wettkampf = new WettkampfViewModel()
            {
                Nachname = wettkampfDB.Nachname,
                Vorname = wettkampfDB.Vorname,
                Kuenstlername = wettkampfDB.Kuenstlername,
                Geburtstag = wettkampfDB.Geburtstag,
                Mitgliedsnummer = wettkampfDB.Mitgliedsnummer,
                HausBerg = wettkampfDB.HausBerg,
                BergList = BergList
            };

            return View(wettkampf);
        }

        [HttpPost]
        public IActionResult Update(WettkampfViewModel wettkampf)
        {
            Wettkampf wettkampfDB;
            using (var Context = new snowboardingContext())
            {
                wettkampfDB = Context.Wettkaempfe.First(x => x.WettkampfID.Equals(wettkampf.WettkampfID));

                wettkampfDB.Name = wettkampf.Name;
                wettkampfDB.Jahr = wettkampf.Jahr;
                wettkampfDB.Berg = wettkampf.Berg;

                Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}