using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class PictureController : BaseController
    {
        // GET: Admin/Picture
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            SetViewBagPost();
            SetViewBagUser();
            return View();
        }
        public void SetViewBagPost(int? selectedID = null)
        {
            var dao = new PictureDAO();
            ViewBag.PostID = new SelectList(dao.GetListPost(), "PostID", "Title", selectedID);
        }
        public void SetViewBagUser(int? selectedID = null)
        {
            var dao = new PictureDAO();
            ViewBag.UserID = new SelectList(dao.GetListUser(), "UserID", "Name", selectedID);
        }
    }
}