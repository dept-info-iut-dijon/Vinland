﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public class Carte
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int ID_Campagne { get; set; }

        public Carte(int ID, string Nom, int ID_Campagne)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.ID_Campagne = ID_Campagne;
        }

    }
}
