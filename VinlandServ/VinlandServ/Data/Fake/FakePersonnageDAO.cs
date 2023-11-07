namespace VinlandServ.Data.Fake
{
    public class FakePersonnageDAO : IPersonnageDAO
    {
        private List<Personnage> personnages;

        public FakePersonnageDAO()
        {
            this.personnages = new List<Personnage>();
        }

        public List<Personnage> GetPersonnages()
        {
            return personnages;
        }

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

        public void NewPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

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
