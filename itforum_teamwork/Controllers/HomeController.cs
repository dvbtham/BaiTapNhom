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

namespace itforum_teamwork.Controllers
{

    public class HomeController : Controller
    {
        private const int defaultPageSize = 10;
        public ActionResult Index(string searchString, int? page)
        {
            CommonConstants.IsAjax = true;
            ViewBag.SearchString = searchString;

            IList<Post> posts = new ArticleDAO().GetPosts();

            int currentPageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                posts = posts.ToPagedList(currentPageIndex, defaultPageSize);
                ViewBag.foundNumbers = posts.Count;
            }
            else
            {
                posts = posts.Where(x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Content.Contains(searchString)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AjaxPostList", posts);
            }
            else
                return View(posts);
        }

        public PartialViewResult Slider()
        {
            ViewBag.Slides = new ArticleDAO().GetHotArticle();
            return PartialView();
        }

    }
}
