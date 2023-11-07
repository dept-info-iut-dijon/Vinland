namespace VinlandServ.Data
{
    public class FakeJoueurDAO : IJoueurDAO
    {
        private List<Joueur> joueurs;

        public FakeJoueurDAO()
        {
            this.joueurs = new List<Joueur>();
        }

        public List<Joueur> GetJoueurs()
        {
            return joueurs;
        }

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

        public void NewJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
        }

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
