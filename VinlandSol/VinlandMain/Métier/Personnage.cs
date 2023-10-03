using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public class Personnage
    {
        private string nom;
        private DateTime datecreation;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public DateTime Datecreation
        {
            get { return datecreation; }
            set { datecreation = value; }
        }

        public Personnage()
        {

        }

        public static void AjouterPersonnage(List<Personnage> listePersonnages, Personnage nouveauPersonnage)
        {
            listePersonnages.Add(nouveauPersonnage);
        }

        public static void SupprimerPersonnage(List<Personnage> listePersonnages, Personnage personnageASupprimer)
        {
            listePersonnages.Remove(personnageASupprimer);
        }
    }
}
