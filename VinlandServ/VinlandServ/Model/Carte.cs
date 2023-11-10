namespace VinlandServ
{
    /// <summary>
    /// Une carte
    /// </summary>
    public class Carte
    {
        public int Id { get; set; }
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        public Campagne Campagne { get; set; } // Campagne à laquelle la Carte appartient

        public Carte(int id, int hauteur, int largeur, Campagne campagne)
        {
            Id = id;
            Hauteur = hauteur;
            Largeur = largeur;
            Campagne = campagne;
        }
    }
}
