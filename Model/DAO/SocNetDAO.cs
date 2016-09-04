using Model.EF;
using System.Linq;

namespace Model.DAO
{
    public class SocNetDAO
    {
        private itforumEntities db = null;

        public SocNetDAO()
        {
            db = new itforumEntities();
        }

        public SocialNetwork GetSocNet(long? id, int socType)
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