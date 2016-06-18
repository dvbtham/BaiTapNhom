using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContactDAO
    {
        itforumEntities db = null;
        public ContactDAO()
        {
            db = new itforumEntities();
        }

        public Contact GetContactInfo()
        {
            return db.Contacts.SingleOrDefault(x => x.Status == true);
        }
    }
}
