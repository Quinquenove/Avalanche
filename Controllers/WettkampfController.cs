using Avalanche.Data;
using Avalanche.Models;
using Avalanche.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Controllers
{
    /// <summary>
    /// Controller für alle Seiten der Wettkampf Kategorie.
    /// Beinhaltet CRUD Funktionen für die Tabellen Wettkampf und Wettkaempfer.
    /// </summary>
    public class WettkampfController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new snowboardingContext());

        public IActionResult Index()
        {
            List<WettkampfViewModel> list = new List<WettkampfViewModel>();

            var wettkampfDB = unitOfWork.Wettkampf.GetAll();

            foreach (var wettkampf in wettkampfDB)
            {
                list.Add(new WettkampfViewModel()
                {
                    Id = wettkampf.Id,
                    Name = wettkampf.Name,
                    Jahr = wettkampf.Jahr,
                    Sponsor = wettkampf.Sponsor.Name,
                    Berg = wettkampf.Berg.Name,
                    Preisgeld = wettkampf.Preisgeld
                });
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var sponsoren = unitOfWork.Sponsor.GetAll();
            var berge = unitOfWork.Berg.GetAll();
            var wettkampf = new WettkampfViewModel()
            {
                Jahr = DateTime.Now.Year,
                SponsorList = sponsoren.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString()}),
                BergList = berge.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(wettkampf);
        }

        [HttpPost]
        public IActionResult Add(WettkampfViewModel wettkampf)
        {
            Wettkampf wettkampfDB = new Wettkampf()
            {
                Name = wettkampf.Name,
                Jahr = wettkampf.Jahr,
                SponsorId = long.Parse(wettkampf.Sponsor),
                BergId = long.Parse(wettkampf.Berg),
                Preisgeld = wettkampf.Preisgeld
            };

            unitOfWork.Wettkampf.Add(wettkampfDB);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(long wettkampf)
        {
            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkampf);

            unitOfWork.Wettkampf.Remove(wettkampfDB);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(long wettkampf)
        {
            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkampf);
            var sponsoren = unitOfWork.Sponsor.GetAll();
            var berge = unitOfWork.Berg.GetAll();

            WettkampfViewModel wettkampfView = new WettkampfViewModel()
            {
                Id = wettkampfDB.Id,
                Name = wettkampfDB.Name,
                Jahr = wettkampfDB.Jahr,
                Sponsor = wettkampfDB.SponsorId.ToString(),
                Berg = wettkampfDB.BergId.ToString(),
                Preisgeld = wettkampfDB.Preisgeld,
                SponsorList = sponsoren.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()}),
                BergList = berge.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(wettkampfView);
        }

        [HttpPost]
        public IActionResult Update(WettkampfViewModel wettkampf)
        {
            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkampf.Id);

            wettkampfDB.Name = wettkampf.Name;
            wettkampfDB.Jahr = wettkampf.Jahr;
            wettkampfDB.SponsorId = long.Parse(wettkampf.Sponsor);
            wettkampfDB.BergId = long.Parse(wettkampf.Berg);
            wettkampfDB.Preisgeld = wettkampf.Preisgeld;

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(long wettkampf)
        {
            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkampf);

            WettkampfViewModel wettkampfView = new WettkampfViewModel
            {
                Id = wettkampfDB.Id,
                Name = wettkampfDB.Name,
                Jahr = wettkampfDB.Jahr,
                Preisgeld = wettkampfDB.Preisgeld
            };

            BergViewModel berg = new BergViewModel
            {
                Id = wettkampfDB.Berg.Id,
                Name = wettkampfDB.Berg.Name,
                GebirgeId = wettkampfDB.Berg.Gebirge.Name,
                SchwierigkeitId = wettkampfDB.Berg.Schwierigkeit.Name
            };

            SponsorViewModel sponsor = new SponsorViewModel
            {
                Name = wettkampfDB.Sponsor.Name
            };

            var snowboarderListe = unitOfWork.Snowboarder.Find(x => x.Wettkampfs.Any(y => y.Id == wettkampf)).Select(x => new SnowboarderViewModel { 
                Mitgliedsnummer = x.Mitgliedsnummer,
                Nachname = x.Nachname,
                Kuenstlername = x.Kuenstlername,
                Vorname = x.Vorname,
                Geburtstag = x.Geburtstag,
                HausBergId = x.HausBergId.ToString()
            });

            WettkampfDetailViewModel wettkampfDetail = new WettkampfDetailViewModel
            {
                Wettkampf = wettkampfView,
                Berg = berg,
                Sponsor = sponsor,
                SnowboarderList = snowboarderListe
            };

            return View(wettkampfDetail);
        }

        [HttpGet]
        public IActionResult AddWettkaempfer(long wettkampf)
        {
            var wettkampfDB =  unitOfWork.Wettkampf.GetById(wettkampf);
            var snowboarderDB = unitOfWork.Snowboarder.GetAll().Where(x => !x.Wettkampfs.Contains(wettkampfDB)).ToList();

            WettkaempferViewModel wettkaempfer = new WettkaempferViewModel
            {
                WettkampfId = wettkampf,
                SnowboarderList = snowboarderDB.Select(x => new SnowboarderViewModel
                {
                    Mitgliedsnummer = x.Mitgliedsnummer,
                    Nachname = x.Nachname,
                    Kuenstlername = x.Kuenstlername,
                    Vorname = x.Vorname,
                    Geburtstag = x.Geburtstag,
                    HausBergId = x.HausBergId.ToString()
                })
            };
            return View(wettkaempfer);
        }

        [HttpPost]
        public IActionResult AddWettkaempfer(WettkaempferViewModel wettkaempfer)
        {
            var selectedSnowboarder = unitOfWork.Snowboarder.Find(x => wettkaempfer.SelectedSnowboarder.Contains(x.Mitgliedsnummer));
            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkaempfer.WettkampfId);

            foreach(var snowboarder in selectedSnowboarder)
            {
                wettkampfDB.Snowboarders.Add(snowboarder);
            }

            unitOfWork.Complete();

            return RedirectToAction("Detail", new {wettkampf = wettkaempfer.WettkampfId});
        }

        public IActionResult DeleteWettkaempfer(string snowboarderID, string wettkampf)
        {
            long wettkampfID = long.Parse(wettkampf);

            var wettkampfDB = unitOfWork.Wettkampf.GetById(wettkampfID);

            var snowboarderDB = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarderID)).First();

            wettkampfDB.Snowboarders.Remove(snowboarderDB);

            unitOfWork.Complete();

            return RedirectToAction("Detail", new {wettkampf = wettkampfID});
        }
    }
}
