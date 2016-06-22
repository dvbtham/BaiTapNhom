using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.PostsCounter = new CategoryDAO().GetItemNumber();
            ViewBag.UsersCounter = new UserDAO().UserCounter();
            ViewBag.TagsCounter = new TagDAO().TagsCounter();
            ViewBag.FeBaCounter = new FeedBackDAO().FeBaCounter();
            return View();
        }
    }
}