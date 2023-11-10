namespace VinlandServ.Data.Fake
{
    /// <summary>
    /// FakeDAO du personnage
    /// </summary>
    public class FakePersonnageDAO : IPersonnageDAO
    {
        private List<Personnage> personnages;

        public FakePersonnageDAO()
        {
            this.personnages = new List<Personnage>();
        }

        /// <summary>
        /// Donne la liste de tout les personnages
        /// </summary>
        /// <returns>Une liste des personnages</returns>
        public List<Personnage> GetPersonnages()
        {
            return personnages;
        }

        /// <summary>
        /// Donne le personnage demandé
        /// </summary>
        /// <param name="id">l'id du personnage demandé</param>
        /// <returns>Un personnage ou null</returns>
        public Personnage GetPersonnage(int id)
        {
            if (personnages.Count == 0)
            {
                return null;
            }

            Personnage personnage = null;
            for (int i = 0; i < personnages.Count; i++)
            {
                if (personnages[i].Id == id) personnage = personnages[i];
            }
            return personnage;
        }

        /// <summary>
        /// Ajoute le personnage donné à la liste des personnages
        /// </summary>
        /// <param name="personnage">le personnage à ajouter</param>
        public void NewPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

        /// <summary>
        /// Ecrase le personnage désigné par le personnage donné
        /// </summary>
        /// <param name="id">l'id du personnage à modifier</param>
        /// <param name="personnage">le nouveau personnage</param>
        public void UpdatePersonnage(int id, Personnage personnage)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].Id == personnage.Id) personnages[i] = personnage;
                }
            }
        }

        /// <summary>
        /// Supprime le personnage avec l'id correspondant de la liste des personnages
        /// </summary>
        /// <param name="id">l'id du personnage à supprimer</param>
        public void DeletePersonnage(int id)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].Id == id) personnages.RemoveAt(i);
                }
            }
        }
    }
}
