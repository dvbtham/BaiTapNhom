using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SocNetDAO
    {
        itforumEntities db = null;
        public SocNetDAO()
        {
            db = new itforumEntities();
        }
        public SocialNetwork GetSocNet(long? id, string socType)
        {
            var model = db.SocialNetworks.SingleOrDefault(x => x.UserID == id && x.SNTypeID == socType && x.Status == true);
            return model;
        }
        public SocialNetwork MySocNet(long? id)
        {
            var model = db.SocialNetworks.SingleOrDefault(x => x.UserID == id && x.Status == true);
            return model;
        }

    }
}
