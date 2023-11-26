using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Une Campagne
    /// </summary>
    public class Campagne
    {
        #region Propriétés

        public int ID { get; set; }
        public string Nom { get; set; } // Nom de la campagne
        public DateTime DateCreation { get; set; } // Date de la création de la campagne
        public DateTime DateModification { get; set; } // Dernière date de modification
        public int IDMJ {  get; set; } // Le MJ propriétaire de la campagne
        public List<int> IDPersonnages { get; set; } // Les ids des personnages de la campagne
        public List<int> IDCartes { get; set; } // Les ids des cartes de la campagne

        #endregion

        #region Constructeur

        public Campagne() { }   

        public Campagne(int id, string nom, int idMJ) 
        {
            ID = id;
            Nom = nom;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
            IDMJ = idMJ;
            IDPersonnages = new List<int>();
            IDCartes = new List<int>();
        }

        #endregion
    }
}

