namespace VinlandServ.Data.Fake
{
    /// <summary>
    /// FakeDAO du MJ
    /// </summary>
    public class FakeMJDAO : IMJDAO
    {
        private List<MJ> mjs;

        public FakeMJDAO()
        {
            this.mjs = new List<MJ>();
        }

        /// <summary>
        /// Donne la liste de tout les mjs
        /// </summary>
        /// <returns>Une liste des mjs</returns>
        public List<MJ> GetMJs()
        {
            return mjs;
        }

        /// <summary>
        /// Donne le mj demandé
        /// </summary>
        /// <param name="id">l'id du mj demandé</param>
        /// <returns>Un mj ou null</returns>
        public MJ GetMJ(int id)
        {
            if (mjs.Count == 0)
            {
                return null;
            }

            MJ mj = null;
            for (int i = 0; i < mjs.Count; i++)
            {
                if (mjs[i].Id == id) mj = mjs[i];
            }
            return mj;
        }

        /// <summary>
        /// Ajoute le mj donné à la liste des mjs
        /// </summary>
        /// <param name="mJ">le mj à ajouter</param>
        public void NewMJ(MJ mJ)
        {
            mjs.Add(mJ);
        }

        /// <summary>
        /// Ecrase le mj désigné par le mj donné
        /// </summary>
        /// <param name="id">l'id du mj à modifier</param>
        /// <param name="mj">le nouveau mj</param>
        public void UpdateMJ(int id, MJ mj)
        {
            if (mjs.Count != 0)
            {
                for (int i = 0; i < mjs.Count; i++)
                {
                    if (mjs[i].Id == mj.Id) mjs[i] = mj;
                }
            }
        }

        /// <summary>
        /// Supprime le mj avec l'id correspondant de la liste des mjs
        /// </summary>
        /// <param name="id">l'id du mj à supprimer</param>
        public void DeleteMJ(int id)
        {
            if (mjs.Count != 0)
            {
                for (int i = 0; i < mjs.Count; i++)
                {
                    if (mjs[i].Id == id) mjs.RemoveAt(i);
                }
            }
        }

    }
}
