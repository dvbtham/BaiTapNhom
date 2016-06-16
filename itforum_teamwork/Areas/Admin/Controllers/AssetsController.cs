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
    public class AssetsController : BaseController
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetViewBag();
            SetTypeName();
            var model = new AssetsDAO().ViewDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Asset model)
        {
            if (ModelState.IsValid)
            {
                var res = new AssetsDAO().Update(model);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Cập nhật thành công", "success");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Lỗi");
            }
            return View("Index");
        }
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new UserDAO();
            ViewBag.UserID = new SelectList(dao.ListUser(), "UserID", "Name", selectedID);
        }
        public void SetTypeName(int? selectedID = null)
        {
            var dao = new AssetsDAO();
            ViewBag.AssetTypeID = new SelectList(dao.ListType(), "AssetTypeID", "AssetName", selectedID);
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = new AssetsDAO().Delete(id);
                if (res)
                {
                    SetAlert("Xóa thành công !", "success");
                    return View("Index");
                }
                else
                {
                    SetAlert("Xóa không thành công !", "error");
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi");
            }
            return View("Index");
        }
    }
}