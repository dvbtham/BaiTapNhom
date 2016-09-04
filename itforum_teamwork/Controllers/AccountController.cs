using itforum_teamwork.Common;
using itforum_teamwork.Models;
using Model.DAO;
using Model.EF;
using System;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var cryptedMD5Pass = Encryptor.MD5Hash(model.Password);
                model.Password = cryptedMD5Pass;
                if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã được đăng ký");
                    return View();
                }
                else
                {
                    var user = new User();
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.CreatedDate = DateTime.Now;
                    user.RoleID = 2;
                    user.ConfirmedByEmail = false;
                    user.Status = true;
                    if (model.Avatar != null)
                    {
                        user.Avatar = model.Avatar;
                    }
                    else
                    {
                        user.Avatar = @"\Data\images\Avatar\default_avatar.png";
                    }
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Message = "Chúc mừng! Bạn đã đăng ký tài khoản thành công.";
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            return View();
        }
    }
}