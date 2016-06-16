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
    public class AssetsController : Controller
    {
        private const int defaultPageSize = 10;
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

        public ActionResult Details(int id)
        {
            var modelFirst = new AssetsDAO().ViewDetail(id);
            if (modelFirst == null)
                return RedirectToAction("Index", "Error");
            return View(modelFirst);
        }
       
        public ActionResult AssetByUser(int? page, long id)
        {
            IList<Asset> assets = new AssetsDAO().ByUserID(id);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            assets = assets.ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxAssetsList", assets);
            else
                return View(assets);
        }
        public ActionResult AssetByAssType(int? page, int id)
        {
            IList<Asset> assets = new AssetsDAO().ByAssetTypeID(id);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            assets = assets.ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxAssetsList", assets);
            else
                return View(assets);
        }
    }
}