namespace VinlandServ.Data
{
    /// <summary>
    /// Interface du DAO de Carte
    /// </summary>
    public interface ICarteDAO
    {
        List<Carte> GetCartes();
        Carte GetCarte(int id);
        void NewCarte(Carte carte);
        void UpdateCarte(int id, Carte carte);
        void DeleteCarte(int id);
    }
}
