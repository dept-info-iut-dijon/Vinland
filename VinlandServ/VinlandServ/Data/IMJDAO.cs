namespace VinlandServ.Data
{
    /// <summary>
    /// Interface du DAO de MJ
    /// </summary>
    public interface IMJDAO
    {
        List<MJ> GetMJs();
        MJ GetMJ(int id);
        void NewMJ(MJ mJ);
        void UpdateMJ(int id, MJ mJ);
        void DeleteMJ(int id);
    }
}
