using itforum_teamwork.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class ManageController : BaseController
    {
        public ActionResult Index()
        {
            var session = (UserLogin)Session[itforum_teamwork.Common.CommonConstants.CLIENT_USER_SESSION];
            var user = new ManageDAO().GetUserByID(Convert.ToInt64(session.UserID));
            return View(user);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var currPass = user.Password;
                var dao = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    if (currPass.Length != 32)
                    {
                        var encryptesMD5Pas = Encryptor.MD5Hash(user.Password);
                        user.Password = encryptesMD5Pas;
                    }
                    else
                        user.Password = currPass;
                }

                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật thông tin cá nhân thành công", "success");
                    var session = (UserLogin)Session[itforum_teamwork.Common.CommonConstants.CLIENT_USER_SESSION];
                    session.Name = user.Name;
                    var userModel = new ManageDAO().GetUserByID(Convert.ToInt64(session.UserID));
                    return View("Index", userModel);
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin cá nhân không thành công");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session.Remove("CLIENT_USER_SESSION");

            if (Response.Cookies["client_email"] != null)
            {
                HttpCookie ckEmail = new HttpCookie("client_email");
                ckEmail.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckEmail);
            }
            if (Response.Cookies["client_password"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("client_password");
                ckPassword.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckPassword);
            }
            return RedirectToAction("Login", "Login");
        }
    }
}