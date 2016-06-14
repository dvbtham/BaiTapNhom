using itforum_teamwork.Areas.Admin.Models;
using itforum_teamwork.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            Account account = CheckCookie();
            if (account == null)
                return View();
            else
            {

                UserDAO userDAO = new UserDAO();
                userDAO.ClientLogin(account.Email, Encryptor.MD5Hash(account.Password));
                var user = userDAO.GetUserByEmail(account.Email);

                AddLoginSession(user);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.ClientLogin(loginModel._Account.Email, Encryptor.MD5Hash(loginModel._Account.Password));
                if (res == 1)
                {
                    var user = dao.GetUserByEmail(loginModel._Account.Email);

                    AddLoginSession(user);

                    if (loginModel.RememberMe)
                    {
                        HttpCookie ckEmail = new HttpCookie("client_email");
                        ckEmail.Expires = DateTime.Now.AddDays(3600);
                        ckEmail.Value = loginModel._Account.Email;
                        Response.Cookies.Add(ckEmail);

                        HttpCookie ckPassword = new HttpCookie("client_password");
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
                                if (res == -2)
                                {
                                    Session["client_Email"] = loginModel._Account.Email;
                                    return RedirectToAction("Index", "Confirm");
                                }
                                else
                                    if (res == -3)
                                    {
                                        ViewBag.Mess = "Bạn đang là Admin! Vui lòng đăng ký thành viên";
                                        return View();
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Đăng nhập không thành công");
                                    }
            }
            return View("Login");
        }

        protected void AddLoginSession(Model.EF.User user)
        {
            var userSession = new UserLogin();
            userSession.Email = user.Email;
            userSession.UserID = user.UserID;
            userSession.Name = user.Name;
            userSession.RoleID = user.RoleID;
            Session["client_userID"] = user.UserID;

            Session.Add(CommonConstants.CLIENT_USER_SESSION, userSession);
        }
        protected Account CheckCookie()
        {
            Account account = null;
            string email = string.Empty, password = string.Empty;

            if (Request.Cookies["client_email"] != null)
                email = Request.Cookies["client_email"].Value;

            if (Request.Cookies["client_password"] != null)
                password = Request.Cookies["client_password"].Value;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                account = new Account { Email = email, Password = password };

            return account;
        }
    }
}