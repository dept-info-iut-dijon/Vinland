namespace VinlandServ.Data.Fake
{
    /// <summary>
    /// FakeDAO de la Carte
    /// </summary>
    public class FakeCarteDAO : ICarteDAO
    {
        private List<Carte> cartes;

        public FakeCarteDAO()
        {
            this.cartes = new List<Carte>();
        }

        /// <summary>
        /// Donne la liste de toutes les cartes
        /// </summary>
        /// <returns>Une liste des cartes</returns>
        public List<Carte> GetCartes()
        {
            return cartes;
        }

        /// <summary>
        /// Donne la carte demandée
        /// </summary>
        /// <param name="id">l'id de la carte demandée</param>
        /// <returns>Une carte ou null</returns>
        public Carte GetCarte(int id)
        {
            if (cartes.Count == 0)
            {
                return null;
            }

            Carte carte = null;
            for (int i = 0; i < cartes.Count; i++)
            {
                if (cartes[i].Id == id) carte = cartes[i];
            }
            return carte;
        }

        /// <summary>
        /// Ajoute la carte donnée à la liste des cartes
        /// </summary>
        /// <param name="carte">la carte à ajouter</param>
        public void NewCarte(Carte carte)
        {
            cartes.Add(carte);
        }

        /// <summary>
        /// Ecrase la carte désignée par la carte donnée
        /// </summary>
        /// <param name="id">l'id de la carte à modifier</param>
        /// <param name="carte">la nouvelle carte</param>
        public void UpdateCarte(int id, Carte carte)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].Id == carte.Id) cartes[i] = carte;
                }
            }
        }

        /// <summary>
        /// Supprime la carte avec l'id correspondant de la liste des cartes
        /// </summary>
        /// <param name="id">l'id de la carte à supprimer</param>
        public void DeleteCarte(int id)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].Id == id) cartes.RemoveAt(i);
                }
            }
        }
    }
}
