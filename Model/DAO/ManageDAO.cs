using Model.EF;
using System.Linq;

namespace Model.DAO
{
    public class ManageDAO
    {
        private itforumEntities db = null;

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