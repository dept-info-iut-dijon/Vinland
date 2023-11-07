namespace VinlandServ
{
    public class Campagne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Personnage> Personnages { get; set; }
        public List<Carte> Cartes { get; set; }

        public Campagne(int id, string nom)
        {
            Id = id;
            Nom = nom;
            Personnages = new List<Personnage>();
            Cartes = new List<Carte>();
        }
    }
}
