namespace VinlandServ.Data
{
    /// <summary>
    /// Interface du DAO de Personnage
    /// </summary>
    public interface IPersonnageDAO
    {
        List<Personnage> GetPersonnages();
        Personnage GetPersonnage(int id);
        void NewPersonnage(Personnage personnage);
        void UpdatePersonnage(int id, Personnage personnage);
        void DeletePersonnage(int id);
    }
}
