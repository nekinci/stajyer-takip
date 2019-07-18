﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajyerTakip.Models
{
    public class BirimKoordinatoru 
    {
        public int ID { get; set; }
        public int ModeratorID { get; set; }
        public Profil Profil { get; set; }
        public int ProfilID { get; set; }
        public int BirimID { get; set; }
        public string Unvan { get; set; }
        public Birim Birim { get; set; }
        public Moderator Moderator { get; set; }
      
        public List<StajyerBirimK> Stajyerler { get; set; }
        public List<Gunluk> Gunlukler { get; set; }
        public List<ProjeBirim> Projeler { get; set; }
        
    }
}
