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
    public class HomeController : Controller
    {

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ArticleDAO();
            var model = dao.GetListOfArticlePagging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.foundNumbers = model.Count();
            if (model == null)
                return RedirectToAction("Index", "Error");
            return View(model);
        }

        public PartialViewResult Slider()
        {
            ViewBag.Slides = new ArticleDAO().GetHotArticle();
            return PartialView();
        }
        [HttpPost]
        public JsonResult myViews(long id)
        {
            var result = new ArticleDAO().AddViews(id);
            return Json(new
            {
                views = result
            });
        }
    }
}
