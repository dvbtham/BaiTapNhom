using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Model.DAO
{
    public class UserDAO
    {
        itforumEntities db = null;
        public UserDAO()
        {
            db = new itforumEntities();
        }
        public List<Role> ListRole()
        {
            return db.Roles.ToList();
        }
        public List<User> ListUser()
        {
            return db.Users.ToList();
        }
        public int UserCounter()
        {
            return db.Users.Count();
        }
        public User GetUserByID(long? id)
        {
            var user = db.Users.SingleOrDefault(x => x.UserID == id);
            return user;
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;

            db.SaveChanges();

            return user.Status;
        }
        public long Insert(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return user.UserID;
        }

        public bool Update(User u)
        {
            try
            {
                var user = db.Users.Find(u.UserID);
                user.Name = u.Name;
                if (!string.IsNullOrEmpty(u.Password))
                {
                    user.Password = u.Password;
                }

                user.Email = u.Email;
                user.Phone = u.Phone;
                user.ConfirmedByEmail = true;
                user.RoleID = 2;
                user.Status = true;
                user.CreatedDate = u.CreatedDate;

                if (!string.IsNullOrEmpty(user.Avatar))
                    user.Avatar = u.Avatar;
                else
                    user.Avatar = "/Data/images/ArticleImg/default_avatar.png";

                user.AboutMe = u.AboutMe;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Actived(User user)
        {
            try
            {
                var userModel = db.Users.Find(user.UserID);
                user.ConfirmedByEmail = true;
                userModel.ConfirmedByEmail = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(User u)
        {
            try
            {
                var user = db.Users.Find(u.UserID);
                user.Name = u.Name;
                if (!string.IsNullOrEmpty(u.Password))
                {
                    user.Password = u.Password;
                }

                user.Email = u.Email;
                user.Phone = u.Phone;
                user.ConfirmedByEmail = u.ConfirmedByEmail;
                user.RoleID = u.RoleID;
                user.Status = u.Status;
                user.CreatedDate = u.CreatedDate;

                if (!string.IsNullOrEmpty(user.Avatar))
                    user.Avatar = u.Avatar;
                else
                    user.Avatar = "/Data/images/ArticleImg/default_avatar.png";

                user.AboutMe = u.AboutMe;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public User GetUserByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        public User ViewDetail(long id)
        {
            return db.Users.Find(id);
        }

        public int Login(string email, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0;
            }

            else
            {
                if (result.Status == false)
                {
                    return 2;
                }
                else
                    if (result.RoleID == 2)
                    {
                        return 3;
                    }
                    else
                        if (result.ConfirmedByEmail == false)
                        {
                            return -2;
                        }

                        else
                        {
                            if (result.Password == password)
                                return 1;
                            else
                                return -1;
                        }
            }
        }
        public int ClientLogin(string email, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return 2;
                }
                else
                    if (result.ConfirmedByEmail == false)
                    {
                        return -2;
                    }
                    else
                        if (result.RoleID == 1)
                        {
                            return -3;
                        }
                        else
                        {
                            if (result.Password == password)
                                return 1;
                            else
                                return -1;
                        }
            }
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
