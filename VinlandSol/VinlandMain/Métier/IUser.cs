namespace VinlandSol.Métier
{
    /// <summary>
    /// Interface d'un utilisateur
    /// </summary>
    public interface IUser
    {
        #region Propriétés

        int ID { get; set; }
        string Nom { get; set; }
        string Mdp { get; set; }

        #endregion
    }
}
