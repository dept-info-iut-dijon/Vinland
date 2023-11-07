namespace VinlandServ.Data.Fake
{
    public class FakeMJDAO : IMJDAO
    {
        private List<MJ> mjs;

        public FakeMJDAO()
        {
            this.mjs = new List<MJ>();
        }

        public List<MJ> GetMJs()
        {
            return mjs;
        }

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

        public void NewMJ(MJ mJ)
        {
            mjs.Add(mJ);
        }

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
