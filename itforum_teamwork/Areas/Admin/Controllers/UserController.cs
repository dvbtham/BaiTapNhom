
using itforum_teamwork.Areas.Admin.Models;
using itforum_teamwork.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private const int defaultPageSize = 5;
        public ActionResult Index(string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IList<User> users = new UserDAO().ListUser();
            if (string.IsNullOrWhiteSpace(searchString))
            {
                users = users.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                users = users.Where(x => x.Name.ToLower().Contains(searchString) || x.Email.Contains(searchString)).ToPagedList(currentPageIndex, defaultPageSize);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_AjaxUserList", users);
            else
                return View(users);
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public JsonResult getUsers()
        //{
        //    var jsonData = new
        //    {
        //        data = new UserDAO().ListUser()
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //} 
        [HttpGet]
        public ActionResult Signup()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserViewModel userModel)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var cryptedMD5Pass = Encryptor.MD5Hash(userModel.Password);
                userModel.Password = cryptedMD5Pass;
                if (dao.CheckEmail(userModel.Email))
                {
                    ModelState.AddModelError("", "Email đã được đăng ký");
                    return View();
                }
                else
                {
                    var user = new User();
                    user.Email = userModel.Email;
                    user.Password = userModel.Password;
                    user.Phone = userModel.Phone;
                    user.Name = userModel.Name;
                    user.CreatedDate = DateTime.Now;
                    user.RoleID = userModel.RoleID;
                    user.Status = true;
                    user.ConfirmedByEmail = false;

                    if (!string.IsNullOrEmpty(userModel.Avatar))
                        user.Avatar = userModel.Avatar;
                    else
                        user.Avatar = "/Data/images/ArticleImg/default_avatar.png";

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        SetAlert("Đăng ký thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                        ModelState.AddModelError("", "Đăng ký không thành công");
                }
            }
            return View();
        }

        public void SetViewBag(int? selectedID = null)
        {
            var dao = new UserDAO();
            ViewBag.RoleID = new SelectList(dao.ListRole(), "RoleID", "RoleName", selectedID);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
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

                var result = dao.Edit(user);
                if (result)
                {
                    SetAlert("Cập nhật thông tin cá nhân thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin cá nhân không thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatusAjax(long id)
        {
            var result = new UserDAO().ChangeStatus(id);

            return Json(new
            {
                status = result
            });
        }
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            SetAlert("Xóa tài khoản thành công", "success");
            return RedirectToAction("Index","User");
        }
    }
}