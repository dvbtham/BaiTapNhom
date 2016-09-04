using itforum_teamwork.Common;
using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class UserDAO
    {
        private itforumEntities db = null;

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
            db.Configuration.ProxyCreationEnabled = false;
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
                ShowError.ErrorMessage(dbEx);
            }
            return user.UserID;
        }

        public long InsertForFacbook(User user)
        {
            try
            {
                var checkEmail = db.Users.SingleOrDefault(x => x.Email == user.Email);
                if (checkEmail == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user.UserID;
                }
                else
                {
                    return checkEmail.UserID;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                ShowError.ErrorMessage(dbEx);
                return 0;
            }
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

        public bool ChangePassword(User user)
        {
            try
            {
                var model = db.Users.Find(user.UserID);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = user.Password;
                }
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