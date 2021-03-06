﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajyerTakip.Attributes;
using StajyerTakip.Models;


namespace StajyerTakip.Controllers
{
    [GirisKontrol]
    public class GunlukController : Controller
    {
        private readonly Context db;
        public GunlukController(Context db)
        {
            this.db = db;
        }
        [GunlukEkleme]
        public IActionResult Ekle()
        {
            return View();
        }

        [GunlukEkleme]
        [HttpPost]
        public IActionResult Ekle(Models.Gunluk gunluk)
        {
            var id = (int)HttpContext.Session.GetInt32("id");
            gunluk.OgrenciID = id;
            gunluk.Tarih = DateTime.UtcNow;
            gunluk.OnayDurumu = -1;
            db.Gunlukler.Add(gunluk);
            db.SaveChanges();
            return Redirect("~/Gunluk/Ekle/" + id);


        }

        [GunlukEkleme]
        public IActionResult Duzenle(int id)
        {

            Models.Gunluk gunluk = db.Gunlukler.ToList().Find(x => x.ID == id);


            int kisiid = (int)HttpContext.Session.GetInt32("id");

            if (kisiid != gunluk.OgrenciID)
                return Redirect("~/Error/AuthProblem");

            if (gunluk.Tarih == DateTime.UtcNow)
                return View(gunluk);
            else
                return Redirect("~/Gunluk/Goruntule/" + id);
        }
        [HttpPost]
        public IActionResult Duzenle(Models.Gunluk gunluk, int id)
        {
            Models.Gunluk anaveri = db.Gunlukler.Find(id);

            anaveri.Baslik = gunluk.Baslik;
            anaveri.Bilgiler = gunluk.Bilgiler;
            anaveri.Tarih = gunluk.Tarih;



            db.SaveChanges();
            return Redirect("~/Gunluk/Listele");
        }

        [Attributes.StajyerID]
        [ServiceFilter(typeof(GunlukListeFiltre))]
        public ActionResult Listele(int id)
        {

            return View();
        }

        [StajyerID]
        [ServiceFilter(typeof(GunlukListeFiltre))]

        public JsonResult GunlukleriCekJson(int id)
        {
            Stajyer stajyer = db.Stajyerler.Where(x => x.ID == id).Include(x => x.Devamsizliklar).Include(x => x.Profil).Include(x => x.Gunlukler).SingleOrDefault();

            return Json(stajyer);
        }

        [ServiceFilter(typeof(GunlukGoruntuleme))]
        public IActionResult Goruntule(int id)
        {
            //Tek bir günlüğü görüntüler.
            Gunluk gunluk = db.Gunlukler.Find(id);

            return View(gunluk);
        }

        [HttpPost]
        [StajyerUstYetki]
        public IActionResult Onayla(int id)
        {
            Gunluk gunluk = db.Gunlukler.Find(id);

            gunluk.OnayDurumu = 1;
            db.SaveChanges();

            return PartialView("_GunlukDurumu", gunluk);
        }

        [StajyerUstYetki]
        [HttpPost]
        public IActionResult Reddet(int id)
        {
            Gunluk gunluk = db.Gunlukler.Find(id);

            gunluk.OnayDurumu = 0;
            db.SaveChanges();

            return PartialView("_GunlukDurumu", gunluk);
        }
    }
}
