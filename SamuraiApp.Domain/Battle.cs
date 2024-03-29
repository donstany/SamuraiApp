﻿using System;
using System.Collections.Generic;

namespace SamuraiApp.Domain
{
   public class Battle
    {
        public Battle()
        {
            SamuraiBattles = new HashSet<SamuraiBattle>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public List<Samurai> Samurais { get; set; }
        public HashSet<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
