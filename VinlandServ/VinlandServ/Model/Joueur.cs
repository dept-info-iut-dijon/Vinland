namespace VinlandServ
{
    public class Joueur : IUser
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public List<Personnage> Personnages { get; set; }

        public Joueur(int id, string nom, string mdp)
        {
            Id = id;
            Nom = nom;
            Mdp = mdp;
            Personnages = new List<Personnage>();
        }

    }
}