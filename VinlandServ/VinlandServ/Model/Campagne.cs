namespace VinlandServ
{
    /// <summary>
    /// Une campagne
    /// </summary>
    public class Campagne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Personnage> Personnages { get; set; } // La liste des personnages de la campagne
        public List<Carte> Cartes { get; set; } // La liste des cartes de la campagne

        public Campagne(int id, string nom)
        {
            Id = id;
            Nom = nom;
            Personnages = new List<Personnage>();
            Cartes = new List<Carte>();
        }
    }
}
