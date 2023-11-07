namespace VinlandServ.Data.Fake
{
    public class FakeCarteDAO : ICarteDAO
    {
        private List<Carte> cartes;

        public FakeCarteDAO()
        {
            this.cartes = new List<Carte>();
        }

        public List<Carte> GetCartes()
        {
            return cartes;
        }

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

        public void NewCarte(Carte carte)
        {
            cartes.Add(carte);
        }

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
