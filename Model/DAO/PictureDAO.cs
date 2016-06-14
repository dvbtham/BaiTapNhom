using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class PictureDAO
    {
        itforumEntities db = null;
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
