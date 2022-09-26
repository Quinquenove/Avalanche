using Avalanche.Data;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Controllers
{
    public class MiscController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sponsor()
        {
            List<SponsorViewModel> sponsors = new List<SponsorViewModel>();

            using(var Context = new snowboardingContext())
            {
                var sponsorsDB = Context.Sponsors.ToList();

                foreach(var sponsor in sponsorsDB)
                {
                    sponsors.Add(new SponsorViewModel() { Name = sponsor.Name });
                }
            }

            return View(sponsors);
        }

        [HttpGet]
        public IActionResult AddSponsor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSponsor(SponsorViewModel sponsor)
        {
            using (var Context = new snowboardingContext())
            {
                Sponsor sponsorDB = new Sponsor()
                {
                    Name = sponsor.Name
                };

                Context.Add(sponsorDB);
                Context.SaveChanges();
            }

            return RedirectToAction("Sponsor");
        }

        public IActionResult DeleteSponsor(string sponsor)
        {
            using (var Context = new snowboardingContext())
            {
                var sponsorDB = Context.Sponsors.First(x => x.Name.Equals(sponsor));

                Context.Remove(sponsorDB);
                Context.SaveChanges();
            }

            return RedirectToAction("Sponsor");
        }

        [HttpGet]
        public IActionResult AddSponsoring(string snowboarderID)
        {
            SponsoringViewModel sponsoring;
            List<SelectListItem> sponsorList = new List<SelectListItem>();
            List<SelectListItem> vertragsartList = new List<SelectListItem>();
            using (var Context = new snowboardingContext())
            {
                var sponsors = Context.Sponsors.ToList();
                var vertragsarten = Context.Vertragsarts.ToList();

                foreach(var sponsor in sponsors)
                {
                    sponsorList.Add(new SelectListItem() { Text = sponsor.Name, Value = sponsor.Name });
                }

                foreach(var vertragsart in vertragsarten)
                {
                    vertragsartList.Add(new SelectListItem() { Text = vertragsart.Name, Value = vertragsart.Name });
                }

                sponsoring = new SponsoringViewModel()
                {
                    Mitgliedsnummer = snowboarderID,
                    SponsorList = sponsorList,
                    VertragsartList = vertragsartList
                };
            }
            return View(sponsoring);
        }

        [HttpPost]
        public IActionResult AddSponsoring(SponsoringViewModel sponsoring)
        {
            using(var Context = new snowboardingContext())
            {
                var sponsoringDB = new Sponsoring()
                {
                    Snowboarder = sponsoring.Mitgliedsnummer,
                    Sponsor = sponsoring.Sponsor,
                    Vertragsart = sponsoring.Vertragsart
                };

                Context.Add(sponsoringDB);
                Context.SaveChanges();
            }

            return RedirectToAction("Detail", "Snowboarder", new { snowboarderID = sponsoring.Mitgliedsnummer });
        }

        public IActionResult VertragsArt()
        {
            List<VertragsartViewModel> vertragsartList = new List<VertragsartViewModel>();
            using(var Context = new snowboardingContext())
            {
                var vertragsartDB = Context.Vertragsarts.ToList();

                foreach(var vertragsart in vertragsartDB)
                {
                    vertragsartList.Add(new VertragsartViewModel()
                    {
                        Name = vertragsart.Name
                    });
                }
            }

            return View(vertragsartList);
        }

        [HttpGet]
        public IActionResult AddVertragsArt()
        {
            return View(new VertragsartViewModel());
        }

        [HttpPost]
        public IActionResult AddVertragsArt(VertragsartViewModel vertragsart)
        {
            using (var Context = new snowboardingContext())
            {
                var vertragsartDB = new Vertragsart()
                {
                    Name = vertragsart.Name
                };

                Context.Add(vertragsartDB);
                Context.SaveChanges();
            }

                return RedirectToAction("VertragsArt");
        }
    }
}
