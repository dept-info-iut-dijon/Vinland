namespace VinlandServ.Data.Fake
{
    public class FakeCampagneDAO : ICampagneDAO
    {
        private List<Campagne> campagnes;

        public FakeCampagneDAO()
        {
            this.campagnes = new List<Campagne>();
        }

        public List<Campagne> GetCampagnes()
        {
            return campagnes;
        }

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

        public void NewCampagne(Campagne campagne)
        {
            campagnes.Add(campagne);
        }

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
