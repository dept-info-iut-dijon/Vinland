namespace VinlandServ.Data
{
    /// <summary>
    /// Interface du DAO de Joueur
    /// </summary>
    public interface IJoueurDAO
    {
        List<Joueur> GetJoueurs();
        Joueur GetJoueur(int id);
        void NewJoueur(Joueur joueur);
        void UpdateJoueur(int id, Joueur joueur);
        void DeleteJoueur(int id);
    }
}
