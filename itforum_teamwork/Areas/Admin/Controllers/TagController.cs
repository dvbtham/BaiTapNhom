using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 100)
        {
            var listTags = new TagDAO().GetTagsPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(listTags);
        }
        

        //Add Tag
        [HttpGet]
        public ActionResult AddTag()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTag(Tag tag)
        {
            if (ModelState.IsValid)
            {
                var dao = new TagDAO();
                var res = dao.AddTag(tag);
                if (res)
                {
                    SetAlert("Tag đã được thêm thành công", "success");
                    return RedirectToAction("Index", "Tag");
                }
                if (res)
                {
                    SetAlert("Tag đã được thêm thành công", "success");
                    return View();
                }

            }
            return View();
        }
        //Edit Tag
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var tag = new TagDAO().ViewDetail(id);
            return View(tag);
        }
        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                var tagDao = new TagDAO();
                var res = tagDao.Edit(tag);
                if (res)
                {
                    SetAlert("Tag đã được cập nhật thành công", "success");
                    return RedirectToAction("Index", "Tag");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin nhập vào không hợp lệ");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            new TagDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}