using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class PostTagController : BaseController
    {
        public ActionResult Index(int pageNumber = 1, int pageSize = 20)
        {
            var postTag = new PostTagDAO().ListPostTagAll(pageNumber, pageSize);

            return View(postTag);
        }
        [HttpGet]
        public ActionResult AddPostTag()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPostTag(PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                var dao = new PostTagDAO();
                var result = dao.AddTag(postTag);
                if (result == 1)
                {
                    SetAlert("Gán tag cho bài viết thành công", "success");
                    return View();
                }
                if (result == -1)
                {
                    SetAlert("Tag này đã tồn tại", "warning");
                    return View();
                }
                if (result == 2)
                {
                    SetAlert("Gán tag cho bài viết không thành công", "error");
                    return View();
                }

            }
            return View();
        }

        public ActionResult Delete(long postID, long tagID)
        {
            var result = new PostTagDAO().Delete(postID, tagID);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa không thành công", "error");
                return View();
            }
        }
    }
}