using itforum_teamwork.Common;
using Model.EF;
using System.Data.Entity.Validation;
using System.Linq;

namespace Model.DAO
{
    public class ContactDAO
    {
        private itforumEntities db = null;

        public ContactDAO()
        {
            db = new itforumEntities();
        }

        public Contact GetContactInfo()
        {
            return db.Contacts.SingleOrDefault(x => x.Status == true);
        }

        public bool Create(Contact model)
        {
            try
            {
                db.Contacts.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                ShowError.ErrorMessage(dbEx);
                return false;
            }
        }

        public bool Edit(Contact model)
        {
            try
            {
                var contact = db.Contacts.Find(model.ContactID);
                contact.Email = model.Email;
                contact.Fax = model.Fax;
                contact.Phone = model.Phone;
                contact.Slogan = model.Slogan;
                contact.Address = model.Address;
                db.Entry(model);
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                ShowError.ErrorMessage(dbEx);
                return false;
            }
        }

        public Contact ViewDetails(int id)
        {
            return db.Contacts.Find(id);
        }
    }
}