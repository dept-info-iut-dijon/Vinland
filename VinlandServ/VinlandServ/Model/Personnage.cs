

namespace VinlandServ
{
    public class Personnage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Joueur Joueur { get; set; }
        public Campagne Campagne { get; set; }

        public Personnage(int id, string nom, Joueur joueur, Campagne campagne)
        {
            this.Id = id;
            this.Nom = nom;
            this.Joueur = joueur;
            this.Campagne = campagne;
        }

    }

}
