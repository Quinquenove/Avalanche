using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avalanche.Data;
using Microsoft.EntityFrameworkCore;
using Avalanche.Repositories;

namespace Avalanche.Controllers
{
    /// <summary>
    /// Controller für alle Seiten der Kategorie Snowboarder.
    /// Beinhaltet CRUD Operationen für die Tabellen Snowboarder und Profi.
    /// </summary>
    public class SnowboarderController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new snowboardingContext());

        public IActionResult Index()
        {
            List<SnowboarderViewModel> list = new List<SnowboarderViewModel>();

            var snowboarders = unitOfWork.Snowboarder.GetAll();

            foreach (var snowboarder in snowboarders)
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

            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var bergDB = unitOfWork.Berg.GetAll();

            var snowboarder = new SnowboarderViewModel
            {
                BergList = bergDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()}),
                Geburtstag = DateTime.Now
            };

            return View(snowboarder);
        }

        [HttpPost]
        public IActionResult Add(SnowboarderViewModel snowboarder)
        {
            Snowboarder snowboarderDB = new Snowboarder()
            {
                Nachname = snowboarder.Nachname,
                Vorname = snowboarder.Vorname,
                Kuenstlername = snowboarder.Kuenstlername,
                Geburtstag = snowboarder.Geburtstag,
                HausBergId = long.Parse(snowboarder.HausBergId),
                Mitgliedsnummer = snowboarder.Mitgliedsnummer
            };

            unitOfWork.Snowboarder.Add(snowboarderDB);
            unitOfWork.Complete();

            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public IActionResult Detail(string snowboarderID)
        {
            SnowboarderDetailViewModel snowboarderDetail = new SnowboarderDetailViewModel();
            List<SponsoringViewModel> sponsoringList = new List<SponsoringViewModel>();

            var snowboarderDB = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarderID)).First();

            SnowboarderViewModel snowboarder = new SnowboarderViewModel()
            {
                Nachname = snowboarderDB.Nachname,
                Vorname = snowboarderDB.Vorname,
                Kuenstlername = snowboarderDB.Kuenstlername,
                Geburtstag = snowboarderDB.Geburtstag,
                HausBergId = snowboarderDB.HausBerg.Name,
                Mitgliedsnummer = snowboarderDB.Mitgliedsnummer
            };

            BergViewModel berg = new BergViewModel()
            {
                GebirgeId = snowboarderDB.HausBerg.Gebirge.Name,
                Name = snowboarderDB.HausBerg.Name,
                SchwierigkeitId = snowboarderDB.HausBerg.Schwierigkeit.Name
            };

            foreach (var sponsor in snowboarderDB.Sponsorings)
            {
                sponsoringList.Add(new SponsoringViewModel()
                {
                    Mitgliedsnummer = sponsor.Snowboarder,
                    Sponsor = sponsor.SponsorNavigation.Name,
                    Vertragsart = sponsor.VertragsartNavigation.Name
                });
            }
            if (snowboarderDB.Profi != null)
            {
                snowboarderDetail.Profi = new ProfiViewModel()
                {
                    Lizenznummer = snowboarderDB.Profi.Lizenznummer,
                    Weltcuppunkte = snowboarderDB.Profi.Weltcuppunkte,
                    Mitgliedsnummer = snowboarderDB.Mitgliedsnummer,
                    BestTrick = snowboarderDB.Profi.BestTrick.Name
                };
            }

            snowboarderDetail.Snowboarder = snowboarder;
            snowboarderDetail.Berg = berg;
            snowboarderDetail.Sponsoring = sponsoringList;

            return View(snowboarderDetail);
        }

        public IActionResult Delete(string snowboarderID)
        {
            var snowboarder = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarderID)).First();

            unitOfWork.Snowboarder.Remove(snowboarder);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(string snowboarderID)
        {
            SnowboarderViewModel snowboarder;
            Snowboarder snowboarderDB;
            snowboarderDB = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarderID)).First();

            var bergDB = unitOfWork.Berg.GetAll();

            snowboarder = new SnowboarderViewModel()
            {
                Nachname = snowboarderDB.Nachname,
                Vorname = snowboarderDB.Vorname,
                Kuenstlername = snowboarderDB.Kuenstlername,
                Geburtstag = snowboarderDB.Geburtstag,
                Mitgliedsnummer = snowboarderDB.Mitgliedsnummer,
                HausBergId = snowboarderDB.HausBerg.Id.ToString(),
                BergList = bergDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(snowboarder);
        }

        [HttpPost]
        public IActionResult Update(SnowboarderViewModel snowboarder)
        {
            Snowboarder snowboarderDB = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarder.Mitgliedsnummer)).First();

            snowboarderDB.Nachname = snowboarder.Nachname;
            snowboarderDB.Vorname = snowboarder.Vorname;
            snowboarderDB.Kuenstlername = snowboarder.Kuenstlername;
            snowboarderDB.Geburtstag = snowboarder.Geburtstag;
            snowboarderDB.HausBergId = long.Parse(snowboarder.HausBergId);

            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddProfi(string snowboarderID)
        {
            var TrickListDB = unitOfWork.Trick.GetAll();

            ProfiViewModel profi = new ProfiViewModel
            {
                Mitgliedsnummer = snowboarderID,
                TrickList = TrickListDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(profi);
        }

        [HttpPost]
        public IActionResult AddProfi(ProfiViewModel profi)
        {
            Profi profiDB = new Profi()
            {
                Lizenznummer = profi.Lizenznummer,
                Weltcuppunkte = profi.Weltcuppunkte,
                Mitgliedsnummer = profi.Mitgliedsnummer,
                BestTrickId = long.Parse(profi.BestTrick)
            };

            unitOfWork.Profi.Add(profiDB);
            unitOfWork.Complete();

            return RedirectToAction("Detail", new { snowboarderID = profi.Mitgliedsnummer });
        }

        [HttpGet]
        public IActionResult UpdateProfi(string profiID)
        {
            ProfiViewModel profi;

            var profiDB = unitOfWork.Profi.Find(x => x.Lizenznummer.Equals(profiID)).First();
            var trickDB = unitOfWork.Trick.GetAll();

            profi = new ProfiViewModel()
            {
                Lizenznummer = profiDB.Lizenznummer,
                Weltcuppunkte = profiDB.Weltcuppunkte,
                Mitgliedsnummer = profiDB.Mitgliedsnummer,
                BestTrick = profiDB.BestTrickId.ToString(),
                TrickList = trickDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(profi);
        }

        [HttpPost]
        public IActionResult UpdateProfi(ProfiViewModel profi)
        {
            var profiDB = unitOfWork.Profi.Find(x => x.Lizenznummer.Equals(profi.Lizenznummer)).First();

            profiDB.Weltcuppunkte = profi.Weltcuppunkte;
            profiDB.BestTrickId = long.Parse(profi.BestTrick);

            unitOfWork.Complete();

            return RedirectToAction("Detail", new { snowboarderID = profi.Mitgliedsnummer });
        }
    }
}