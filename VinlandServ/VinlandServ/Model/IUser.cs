namespace VinlandServ
{
    /// <summary>
    /// Interface des différents types de User
    /// </summary>
    public interface IUser
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Mdp { get; set; }
    }
}
