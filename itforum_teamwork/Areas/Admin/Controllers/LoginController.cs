using itforum_teamwork.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.DAO;
using System.Web.Mvc;
using itforum_teamwork.Common;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Account account = CheckCookie();
            if (account == null)
                return View();
            else
            {

                UserDAO userDAO = new UserDAO();
                userDAO.Login(account.Email, Encryptor.MD5Hash(account.Password));
                var user = userDAO.GetUserByEmail(account.Email);

                var userSession = new UserLogin();
                userSession.Email = user.Email;
                userSession.UserID = user.UserID;

                Session.Add(CommonConstants.ADMIN_USER_SESSION, userSession);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.Login(loginModel._Account.Email, Encryptor.MD5Hash(loginModel._Account.Password));
                if (res == 1)
                {

                    var user = dao.GetUserByEmail(loginModel._Account.Email);

                    var userSession = new UserLogin();
                    userSession.Email = user.Email;
                    userSession.UserID = user.UserID;
                    Session["userID"] = user.UserID; ;

                    Session.Add(CommonConstants.ADMIN_USER_SESSION, userSession);
                    if (loginModel.RememberMe)
                    {
                        HttpCookie ckEmail = new HttpCookie("email");
                        ckEmail.Expires = DateTime.Now.AddDays(3600);
                        ckEmail.Value = loginModel._Account.Email;
                        Response.Cookies.Add(ckEmail);

                        HttpCookie ckPassword = new HttpCookie("password");
                        ckPassword.Expires = DateTime.Now.AddDays(3600);
                        ckPassword.Value = loginModel._Account.Password;
                        Response.Cookies.Add(ckPassword);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                    if (res == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }
                    else
                        if (res == 2)
                        {
                            ModelState.AddModelError("", "Tài khoản của bạn đang bị khóa");
                        }
                        else
                            if (res == -1)
                            {
                                ModelState.AddModelError("", "Sai mật khẩu");
                            }
                            else
                                if (res == 3)
                                {
                                    ModelState.AddModelError("", "Bạn không có quyền này");
                                }
                                else
                                    if (res == -2)
                                    {
                                        return RedirectToAction("Index", "Confirm");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Đăng nhập không thành công");
                                    }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Remove("ADMIN_USER_SESSION");
            Session["userID"] = null;

            if (Response.Cookies["email"] != null)
            {
                HttpCookie ckEmail = new HttpCookie("email");
                ckEmail.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckEmail);
            }
            if (Response.Cookies["password"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("password");
                ckPassword.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckPassword);
            }
            return View("Index");
        }
        public Account CheckCookie()
        {
            Account account = null;
            string email = string.Empty, password = string.Empty;

            if (Request.Cookies["email"] != null)
                email = Request.Cookies["email"].Value;

            if (Request.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                account = new Account { Email = email, Password = password };

            return account;
        }

    }
}