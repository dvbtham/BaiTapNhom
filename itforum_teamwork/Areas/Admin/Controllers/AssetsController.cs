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
    public class AssetsController : Controller
    {
        private const int defaultPageSize = 5;
        public ActionResult Index(string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            IList<Asset> assets = new AssetsDAO().ListAll();
            int currentPageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                assets = assets.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                assets = assets.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxAssetsList", assets);
            else
                return View(assets);
        }
    }
}