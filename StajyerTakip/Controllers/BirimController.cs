﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StajyerTakip.Controllers
{
    public class Birim : Controller
    {
        private readonly Context db;
        public Birim (Context db)
        {
            this.db = db;
        }
        // GET: /<controller>/
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Models.Birim birim)
        {
           
            db.Birimler.Add(birim);
            db.SaveChanges();
            return RedirectToAction("Ekle");

        }
    }
}
