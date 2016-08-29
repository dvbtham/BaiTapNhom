using itforum_teamwork.Areas.Admin.Models;
using itforum_teamwork.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;
using System.Web.Mvc;
using System.Configuration;
using Model.EF;

namespace itforum_teamwork.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string ReturnUrl)
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
                    loginModel.ReturnUrl = ReturnUrl;
                    if (!string.IsNullOrEmpty(loginModel.ReturnUrl))
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                    if (res == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại. Vui lòng tạo mới tài khoản");
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

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        //Login with facebook
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["SecretKey"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope="email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["SecretKey"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            if(!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email,about");
                string email = me.email;
                string about = me.about;
                string id = me.id;
                string username = me.email;
                string firstname = me.first_name;
                string lastname = me.last_name;
                string middlename = me.middle_name;

                var user = new User();
                user.Email = email;
                user.Name = firstname +" "+ middlename +" "+ lastname;
                user.Status = true;
                user.CreatedDate = DateTime.Now;
                user.RoleID = 2;
                user.AboutMe = about;
                user.Password = Encryptor.MD5Hash(id);
                user.ConfirmedByEmail = true;
                user.Avatar = "/Data/images/Avatar/default_avatar.png";
               

                var resultInsert = new UserDAO().InsertForFacbook(user);
                user.UserID = resultInsert;
                if(resultInsert > 0)
                {
                    AddLoginSession(user);
                }
            }
            return Redirect("/");
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