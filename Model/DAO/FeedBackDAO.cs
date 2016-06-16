using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class FeedBackDAO
    {
        itforumEntities db= null;
        public FeedBackDAO()
        {
            db= new itforumEntities();
        }
        public int FeBaCounter()
        {
            return db.Feedbacks.Count();
        }
    }
}
