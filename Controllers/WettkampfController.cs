using Avalanche.Data;
using Avalanche.Models;
using Avalanche.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Controllers
{
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
    }
}
