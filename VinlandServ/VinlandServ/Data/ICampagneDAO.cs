namespace VinlandServ.Data
{
    /// <summary>
    /// Interface du DAO de Campagne
    /// </summary>
    public interface ICampagneDAO
    {
        List<Campagne> GetCampagnes();
        Campagne GetCampagne(int id);
        void NewCampagne(Campagne campagne);
        void UpdateCampagne(int id, Campagne campagne);
        void DeleteCampagne(int id);
    }
}
