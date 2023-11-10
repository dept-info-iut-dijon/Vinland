

namespace VinlandServ
{
    /// <summary>
    /// Un personnage
    /// </summary>
    public class Personnage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Joueur Joueur { get; set; } // Le Joueur propriétaire de ce Personnage
        public Campagne Campagne { get; set; } // La Campagne dont le Personnage fait partie

        public Personnage(int id, string nom, Joueur joueur, Campagne campagne)
        {
            this.Id = id;
            this.Nom = nom;
            this.Joueur = joueur;
            this.Campagne = campagne;
        }

    }

}
