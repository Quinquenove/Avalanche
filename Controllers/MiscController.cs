using Avalanche.Data;
using Avalanche.Models;
using Avalanche.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Controllers
{
    public class MiscController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new snowboardingContext());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sponsor()
        {
            List<SponsorViewModel> sponsors = new List<SponsorViewModel>();

            var sponsorsDB = unitOfWork.Sponsor.GetAll();

            foreach (var sponsor in sponsorsDB)
            {
                sponsors.Add(new SponsorViewModel() { Id = sponsor.Id ,Name = sponsor.Name });
            }

            return View(sponsors);
        }

        [HttpGet]
        public IActionResult AddSponsor()
        {
            return View(new SponsorViewModel());
        }

        [HttpPost]
        public IActionResult AddSponsor(SponsorViewModel sponsor)
        {
            Sponsor sponsorDB = new Sponsor()
            {
                Name = sponsor.Name
            };

            unitOfWork.Sponsor.Add(sponsorDB);
            unitOfWork.Complete();

            return RedirectToAction("Sponsor");
        }

        public IActionResult DeleteSponsor(long sponsor)
        {
            var sponsorDB = unitOfWork.Sponsor.GetById(sponsor);

            unitOfWork.Sponsor.Remove(sponsorDB);
            unitOfWork.Complete();

            return RedirectToAction("Sponsor");
        }

        [HttpGet]
        public IActionResult AddSponsoring(string snowboarderID)
        {
            var sponsorDB = unitOfWork.Sponsor.GetAll();
            var vertragsartDB = unitOfWork.Vertragsart.GetAll();

            SponsoringViewModel sponsoring = new SponsoringViewModel()
            {
                Mitgliedsnummer = snowboarderID,
                SponsorList = sponsorDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()}),
                VertragsartList = vertragsartDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(sponsoring);
        }

        [HttpPost]
        public IActionResult AddSponsoring(SponsoringViewModel sponsoring)
        {
            var sponsoringDB = new Sponsoring()
            {
                Snowboarder = sponsoring.Mitgliedsnummer,
                Sponsor = long.Parse(sponsoring.Sponsor),
                Vertragsart = long.Parse(sponsoring.Vertragsart)
            };

            unitOfWork.Sponsoring.Add(sponsoringDB);
            unitOfWork.Complete();

            return RedirectToAction("Detail", "Snowboarder", new { snowboarderID = sponsoring.Mitgliedsnummer });
        }

        public IActionResult Vertragsart()
        {
            List<VertragsartViewModel> vertragsartList = new List<VertragsartViewModel>();
            var vertragsartDB = unitOfWork.Vertragsart.GetAll();

            foreach (var vertragsart in vertragsartDB)
            {
                vertragsartList.Add(new VertragsartViewModel()
                {
                    Id = vertragsart.Id,
                    Name = vertragsart.Name
                });
            }

            return View(vertragsartList);
        }

        [HttpGet]
        public IActionResult AddVertragsart()
        {
            return View(new VertragsartViewModel());
        }

        [HttpPost]
        public IActionResult AddVertragsart(VertragsartViewModel vertragsart)
        {
            var vertragsartDB = new Vertragsart()
            {
                Name = vertragsart.Name
            };

            unitOfWork.Vertragsart.Add(vertragsartDB);
            unitOfWork.Complete();

            return RedirectToAction("Vertragsart");
        }

        public IActionResult DeleteVertragsart(long vertragsart)
        {
            var vertragsartDB = unitOfWork.Vertragsart.GetById(vertragsart);

            unitOfWork.Vertragsart.Remove(vertragsartDB);
            unitOfWork.Complete();

            return RedirectToAction("Vertragsart");
        }

        public IActionResult Berg()
        {
            List<BergViewModel> bergList = new List<BergViewModel>();

            var bergDB = unitOfWork.Berg.GetAll();

            foreach (var berg in bergDB)
            {
                bergList.Add(new BergViewModel()
                {
                    Id = berg.Id,
                    Name = berg.Name,
                    GebirgeId = berg.Gebirge.Name,
                    SchwierigkeitId = berg.Schwierigkeit.Name
                });
            }

            return View(bergList);
        }

        [HttpGet]
        public IActionResult AddBerg()
        {
            var GebirgeDB = unitOfWork.Gebirge.GetAll();
            var SchwierigkeitDB = unitOfWork.Schwierigkeit.GetAll();

            BergViewModel berg = new BergViewModel
            {
                GebirgeListe = GebirgeDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                SchwierigkeitListe = SchwierigkeitDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(berg);
        }

        [HttpPost]
        public IActionResult AddBerg(BergViewModel berg)
        {
            var bergDB = new Berg()
            {
                Name = berg.Name,
                GebirgeId = long.Parse(berg.GebirgeId),
                SchwierigkeitId = long.Parse(berg.SchwierigkeitId)
            };

            unitOfWork.Berg.Add(bergDB);
            unitOfWork.Complete();

            return RedirectToAction("Berg");
        }

        [HttpGet]
        public IActionResult UpdateBerg(long berg)
        {
            var bergDB = unitOfWork.Berg.GetById(berg);

            var GebirgeListeDB = unitOfWork.Gebirge.GetAll();
            var SchwierigkeitListeDB = unitOfWork.Schwierigkeit.GetAll();

            BergViewModel bergViewModel = new BergViewModel()
            {
                Id = bergDB.Id,
                Name = bergDB.Name,
                GebirgeId = bergDB.GebirgeId.ToString(),
                SchwierigkeitId = bergDB.SchwierigkeitId.ToString(),
                GebirgeListe = GebirgeListeDB.Select(x => new SelectListItem{ Text = x.Name, Value = x.Id.ToString()}),
                SchwierigkeitListe = SchwierigkeitListeDB.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(bergViewModel);
        }

        [HttpPost]
        public IActionResult UpdateBerg(BergViewModel berg)
        {
            var bergDB = unitOfWork.Berg.GetById(berg.Id.Value);

            bergDB.Name = berg.Name;
            bergDB.GebirgeId = long.Parse(berg.GebirgeId);
            bergDB.SchwierigkeitId = long.Parse(berg.SchwierigkeitId);

            unitOfWork.Complete();

            return RedirectToAction("Berg");
        }

        public IActionResult DeleteBerg(long berg)
        {
            var bergDB = unitOfWork.Berg.GetById(berg);

            unitOfWork.Berg.Remove(bergDB);
            unitOfWork.Complete();

            return RedirectToAction("Berg");
        }

        public IActionResult Gebirge()
        {
            List<GebirgeViewModel> gebirgeList = new List<GebirgeViewModel>();

            var gebirgeDB = unitOfWork.Gebirge.GetAll();

            foreach(var gebirge in gebirgeDB)
            {
                gebirgeList.Add(new GebirgeViewModel() { Id = gebirge.Id, Name = gebirge.Name });
            }

            return View(gebirgeList);
        }

        [HttpGet]
        public IActionResult AddGebirge()
        {
            return View(new GebirgeViewModel());
        }

        [HttpPost]
        public IActionResult AddGebirge(GebirgeViewModel gebirge)
        {
            Gebirge gebirgeDB = new Gebirge() { Name = gebirge.Name };

            unitOfWork.Gebirge.Add(gebirgeDB);
            unitOfWork.Complete();

            return RedirectToAction("Gebirge");
        }

        public IActionResult DeleteGebirge(long gebirge)
        {
            var gebirgeDB = unitOfWork.Gebirge.GetById(gebirge);

            unitOfWork.Gebirge.Remove(gebirgeDB);
            unitOfWork.Complete();

            return RedirectToAction("Gebirge");
        }

        public IActionResult Schwierigkeit()
        {
            List<SchwierigkeitViewModel> schwierigkeitList = new List<SchwierigkeitViewModel>();

            var SchwierigkeitDB = unitOfWork.Schwierigkeit.GetAll();

            foreach(var schwierigkeit in SchwierigkeitDB)
            {
                schwierigkeitList.Add(new SchwierigkeitViewModel() { Id = schwierigkeit.Id, Name = schwierigkeit.Name });
            }

            return View(schwierigkeitList);
        }

        [HttpGet]
        public IActionResult AddSchwierigkeit()
        {
            return View(new SchwierigkeitViewModel());
        }

        [HttpPost]
        public IActionResult AddSchwierigkeit(SchwierigkeitViewModel schwierigkeit)
        {
            Schwierigkeit schwierigkeitDB = new Schwierigkeit()
            {
                Name = schwierigkeit.Name
            };

            unitOfWork.Schwierigkeit.Add(schwierigkeitDB);
            unitOfWork.Complete();

            return RedirectToAction("Schwierigkeit");
        }

        public IActionResult DeleteSchwierigkeit(long schwierigkeit)
        {
            var schwierigkeitDB = unitOfWork.Schwierigkeit.GetById(schwierigkeit);

            unitOfWork.Schwierigkeit.Remove(schwierigkeitDB);
            unitOfWork.Complete();

            return RedirectToAction("Schwierigkeit");
        }

        public IActionResult Trick()
        {
            List<TrickViewModel> TrickList = new List<TrickViewModel>();

            var TrickDB = unitOfWork.Trick.GetAll();

            foreach (var trick in TrickDB)
            {
                TrickList.Add(new TrickViewModel() { Id = trick.Id, Name = trick.Name, Beschreibung =  trick.Beschreibung});
            }

            return View(TrickList);
        }

        [HttpGet]
        public IActionResult AddTrick()
        {
            return View(new TrickViewModel());
        }

        [HttpPost]
        public IActionResult AddTrick(TrickViewModel trick)
        {
            Trick trickDB = new Trick()
            {
                Name = trick.Name,
                Beschreibung = trick.Beschreibung
            };

            unitOfWork.Trick.Add(trickDB);
            unitOfWork.Complete();

            return RedirectToAction("Trick");
        }

        public IActionResult DeleteTrick(long trick)
        {
            var trickDB = unitOfWork.Trick.GetById(trick);

            unitOfWork.Trick.Remove(trickDB);
            unitOfWork.Complete();

            return RedirectToAction("Trick");
        }

        [HttpGet]
        public IActionResult UpdateTrick(long trick)
        {
            var trickDB = unitOfWork.Trick.GetById(trick);

            return View(new TrickViewModel() { Id = trickDB.Id, Name = trickDB.Name, Beschreibung = trickDB.Beschreibung});
        }

        [HttpPost]
        public IActionResult UpdateTrick(TrickViewModel trick)
        {
            Trick trickDB = unitOfWork.Trick.GetById(trick.Id.Value);

            trickDB.Name = trick.Name;
            trickDB.Beschreibung = trick.Beschreibung;

            unitOfWork.Complete();

            return RedirectToAction("Trick");
        }
    }
}