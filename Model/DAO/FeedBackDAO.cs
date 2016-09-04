using Model.EF;
using System.Linq;

namespace Model.DAO
{
    public class FeedBackDAO
    {
        private itforumEntities db = null;

        public FeedBackDAO()
        {
            db = new itforumEntities();
        }

        public int FeBaCounter()
        {
            return db.Feedbacks.Count();
        }
    }
}