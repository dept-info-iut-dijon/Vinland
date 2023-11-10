namespace VinlandServ.Data.Fake
{
    /// <summary>
    /// FakeDAO de la Campagne
    /// </summary>
    public class FakeCampagneDAO : ICampagneDAO
    {
        private List<Campagne> campagnes;

        public FakeCampagneDAO()
        {
            this.campagnes = new List<Campagne>();
        }

        /// <summary>
        /// Donne la liste de toutes les campagnes
        /// </summary>
        /// <returns>Une liste des campagnes</returns>
        public List<Campagne> GetCampagnes()
        {
            return campagnes;
        }

        /// <summary>
        /// Donne la campagne demandée
        /// </summary>
        /// <param name="id">l'id de la campagne demandée</param>
        /// <returns>Une campagne ou null</returns>
        public Campagne GetCampagne(int id)
        {
            if (campagnes.Count == 0)
            {
                return null;
            }

            Campagne campagne = null;
            for (int i = 0; i < campagnes.Count; i++)
            {
                if (campagnes[i].Id == id) campagne = campagnes[i];
            }
            return campagne;
        }

        /// <summary>
        /// Ajoute la campagne donnée à la liste des campagnes
        /// </summary>
        /// <param name="campagne">la campagne à ajouter</param>
        public void NewCampagne(Campagne campagne)
        {
            campagnes.Add(campagne);
        }

        /// <summary>
        /// Ecrase la campagne désignée par la campagne donnée
        /// </summary>
        /// <param name="id">l'id de la campagne à modifier</param>
        /// <param name="campagne">la nouvelle campagne</param>
        public void UpdateCampagne(int id, Campagne campagne)
        {
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].Id == campagne.Id) campagnes[i] = campagne;
                }
            }
        }

        /// <summary>
        /// Supprime la campagne avec l'id correspondant de la liste des campagnes
        /// </summary>
        /// <param name="id">l'id de la campagne à supprimer</param>
        public void DeleteCampagne(int id)
        {
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].Id == id) campagnes.RemoveAt(i);
                }
            }
        }
    }
}
