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
                BergList.Add(new SelectListItem() { Value = item.Name, Text = item.Name });
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
                HausBerg = snowboarder.HausBerg,
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
                                    .Include(x => x.HausBergNavigation)
                                    .Include(x => x.Profi)
                                    .Include(x => x.Sponsorings)
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
                foreach(var sponsor in snowboarderDB.Sponsorings)
                {
                    sponsoringList.Add(new SponsoringViewModel()
                    {
                        Mitgliedsnummer = sponsor.Snowboarder,
                        Sponsor = sponsor.Sponsor,
                        Vertragsart = sponsor.Vertragsart
                    });
                }
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
                snowboarderDetail.Sponsoring = sponsoringList;

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

        [HttpGet]
        public IActionResult UpdateProfi(string profiID)
        {
            ProfiViewModel profi;
            using(var context = new snowboardingContext())
            {
                var profiDB = context.Profis.First(x => x.Lizenznummer.Equals(profiID));

                profi = new ProfiViewModel()
                {
                    Lizenznummer = profiDB.Lizenznummer,
                    Weltcuppunkte = profiDB.Weltcuppunkte,
                    Mitgliedsnummer = profiDB.Mitgliedsnummer,
                    BestTrick = profiDB.BestTrick
                };
            }

            return View(profi);
        }

        [HttpPost]
        public IActionResult UpdateProfi(ProfiViewModel profi)
        {
            using(var context = new snowboardingContext())
            {
                var profiDB = context.Profis.First(x => x.Lizenznummer.Equals(profi.Lizenznummer));

                profiDB.Weltcuppunkte = profi.Weltcuppunkte;
                profiDB.BestTrick = profi.BestTrick;

                context.SaveChanges();
            }

            return RedirectToAction("Detail", new { snowboarderID = profi.Mitgliedsnummer });
        }
    }
}