namespace VinlandServ
{
    /// <summary>
    /// Lists Repository 
    /// </summary>
    public class Context
    {
        public List <Campagne> Campagnes = new List<Campagne>();

        public List <Carte> Cartes = new List<Carte>();

        public List <Joueur> Joueurs = new List<Joueur>();

        public List <MJ> MJS = new List<MJ>();

        public List <Personnage> Personnages = new List<Personnage>();
    }
}
