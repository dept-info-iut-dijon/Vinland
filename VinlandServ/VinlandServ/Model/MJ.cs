namespace VinlandServ
{
    public class MJ : IUser
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }

        public List<Campagne> campagnes { get; set; }

        public MJ(int id, string nom, string mdp)
        {
            Id = id;
            Nom = nom;
            Mdp = mdp;
            campagnes = new List<Campagne>();
        }
    }
}