using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ManageDAO
    {
        itforumEntities db = null;
        public ManageDAO()
        {
            db = new itforumEntities();
        }
        
        public User GetUserByID(long? id)
        {
            return db.Users.SingleOrDefault(x => x.UserID == id);
        }
    }
}
