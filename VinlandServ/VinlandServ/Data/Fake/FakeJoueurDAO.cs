namespace VinlandServ.Data
{
    /// <summary>
    /// FakeDAO du Joueur
    /// </summary>
    public class FakeJoueurDAO : IJoueurDAO
    {
        private List<Joueur> joueurs;

        public FakeJoueurDAO()
        {
            this.joueurs = new List<Joueur>();
        }

        /// <summary>
        /// Donne la liste de tout les joueurs
        /// </summary>
        /// <returns>Une liste des joueurs</returns>
        public List<Joueur> GetJoueurs()
        {
            return joueurs;
        }

        /// <summary>
        /// Donne le joueur demandé
        /// </summary>
        /// <param name="id">l'id du joueur demandé</param>
        /// <returns>Un joueur ou null</returns>
        public Joueur GetJoueur(int id)
        {
            if (joueurs.Count == 0)
            {
                return null;
            }

            Joueur joueur = null;
            for (int i = 0; i < joueurs.Count; i++)
            {
                if (joueurs[i].Id == id) joueur = joueurs[i];
            }
            return joueur;
        }

        /// <summary>
        /// Ajoute le joueur donné à la liste des joueurs
        /// </summary>
        /// <param name="joueur">le joueur à ajouter</param>
        public void NewJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
        }

        /// <summary>
        /// Ecrase le joueur désigné par le joueur donné
        /// </summary>
        /// <param name="id">l'id du joueur à modifier</param>
        /// <param name="joueur">le nouveau joueur</param>
        public void UpdateJoueur(int id, Joueur joueur)
        {
            if (joueurs.Count != 0)
            {
                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueurs[i].Id == joueur.Id) joueurs[i] = joueur;
                }
            }
        }

        /// <summary>
        /// Supprime le joueur avec l'id correspondant de la liste des joueurs
        /// </summary>
        /// <param name="id">l"id du joueur à supprimer</param>
        public void DeleteJoueur(int id)
        {
            if (joueurs.Count != 0)
            {
                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueurs[i].Id == id) joueurs.RemoveAt(i);
                }
            }
        }
    }
}
