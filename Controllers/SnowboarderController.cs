﻿using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avalanche.Data;
using Microsoft.EntityFrameworkCore;
using Avalanche.Repositories;

namespace Avalanche.Controllers
{
    public class SnowboarderController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new snowboardingContext());

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
            List<SelectListItem> BergList = new List<SelectListItem>();

            var bergDataList = unitOfWork.Berg.GetAll();

            foreach (var item in bergDataList)
            {
                BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Id.ToString() });
            }

            return View(new SnowboarderViewModel() { BergList = BergList, Geburtstag= DateTime.Now });
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
                HausBergId = long.Parse(snowboarder.HausBerg),
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
            SnowboarderViewModel snowboarder;
            BergViewModel berg;
            List<SponsoringViewModel> sponsoringList = new List<SponsoringViewModel>();
            using (var Context = new snowboardingContext())
            {
                var snowboarderDB = Context.Snowboarders
                                    .Include(x => x.Profi)
                                    .Include(x => x.Sponsorings)
                                    .First(x => x.Mitgliedsnummer.Equals(snowboarderID));
                snowboarder = new SnowboarderViewModel()
                {
                    Nachname = snowboarderDB.Nachname,
                    Vorname = snowboarderDB.Vorname,
                    Kuenstlername = snowboarderDB.Kuenstlername,
                    Geburtstag = snowboarderDB.Geburtstag,
                    HausBerg = snowboarderDB.HausBerg.Name,
                    Mitgliedsnummer = snowboarderDB.Mitgliedsnummer
                };
                berg = new BergViewModel()
                {
                    Gebirge = snowboarderDB.HausBerg.Gebirge.Name,
                    Name = snowboarderDB.HausBerg.Name,
                    Schwierigkeit = snowboarderDB.HausBerg.Schwierigkeit.Name
                };
                foreach(var sponsor in snowboarderDB.Sponsorings)
                {
                    sponsoringList.Add(new SponsoringViewModel()
                    {
                        Mitgliedsnummer = sponsor.Snowboarder,
                        Sponsor = sponsor.SponsorNavigation.Name,
                        Vertragsart = sponsor.VertragsartNavigation.Name
                    });
                }
                if(snowboarderDB.Profi != null)
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

            }
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
            List<SelectListItem> BergList = new List<SelectListItem>();
            snowboarderDB = unitOfWork.Snowboarder.Find(x => x.Mitgliedsnummer.Equals(snowboarderID)).First();

            var bergDataList = unitOfWork.Berg.GetAll();

            foreach (var item in bergDataList)
            {
                BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Id.ToString() });
            }

            snowboarder = new SnowboarderViewModel()
            {
                Nachname = snowboarderDB.Nachname,
                Vorname = snowboarderDB.Vorname,
                Kuenstlername = snowboarderDB.Kuenstlername,
                Geburtstag = snowboarderDB.Geburtstag,
                Mitgliedsnummer = snowboarderDB.Mitgliedsnummer,
                HausBerg = snowboarderDB.HausBerg.Id.ToString(),
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
                snowboarderDB.HausBergId = long.Parse(snowboarder.HausBerg);

                Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddProfi(string snowboarderID)
        {
            List<SelectListItem> TrickList = new List<SelectListItem>();
            var TrickListDB = unitOfWork.Trick.GetAll();

            foreach(var trick in TrickListDB)
            {
                TrickList.Add(new SelectListItem() { Text = trick.Name, Value = trick.Id.ToString() });
            }

            return View(new ProfiViewModel() { Mitgliedsnummer = snowboarderID, TrickList = TrickList});
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

            List<SelectListItem> TrickList = new List<SelectListItem>();

            foreach(var trick in trickDB)
            {
                TrickList.Add(new SelectListItem() { Text = trick.Name, Value = trick.Id.ToString() });
            }

            profi = new ProfiViewModel()
            {
                Lizenznummer = profiDB.Lizenznummer,
                Weltcuppunkte = profiDB.Weltcuppunkte,
                Mitgliedsnummer = profiDB.Mitgliedsnummer,
                BestTrick = profiDB.BestTrick.Name,
                TrickList = TrickList
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