namespace VinlandServ
{
    public interface IUser
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Mdp { get; set; }
    }
}
