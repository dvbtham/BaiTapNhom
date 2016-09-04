using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class PictureDAO
    {
        private itforumEntities db = null;

        public PictureDAO()
        {
            db = new itforumEntities();
        }

        public List<Post> GetListPost()
        {
            return db.Posts.ToList();
        }

        public List<User> GetListUser()
        {
            return db.Users.ToList();
        }
    }
}