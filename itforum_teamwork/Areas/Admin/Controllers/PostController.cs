using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using MvcPaging;
using Model.EF;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private const int defaultPageSize = 10;
        public ActionResult Index(string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            IList<Post> posts = new ArticleDAO().ListPostAll();
            int currentPageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                posts = posts.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                posts = posts.Where(x => x.Title.ToLower().Contains(searchString)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxPostList",posts);
            else
                return View(posts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Post postModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDAO();
                var post = new Post();
                post.Title = postModel.Title;
                post.UserID = Convert.ToInt64(Session["userID"]);
                post.Content = postModel.Content;
                post.Detail = postModel.Detail;
                post.CategoryID = postModel.CategoryID;
                post.PostedDate = DateTime.Now;
                post.Status = true;
                post.Views = 0;
                if (!string.IsNullOrEmpty(post.ImageShowOnHome))
                    post.ImageShowOnHome = postModel.ImageShowOnHome;
                else
                    post.ImageShowOnHome = "/Data/images/ArticleImg/no_image.png";

                var result = dao.Insert(post);
                if (result > 0)
                {
                    SetAlert("Đăng bài viết thành công", "success");
                    return RedirectToAction("Index", "Home", "Admin");
                }
                else
                    ModelState.AddModelError("", "Đăng bài viết không thành công");
            }
            return View();
        }
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "CategoryID", "CategoryName", selectedID);
        }
        public void SetUserName(int? selectedID = null)
        {
            var dao = new UserDAO();
            ViewBag.UserID = new SelectList(dao.ListUser(), "UserID", "Name", selectedID);
        }

        [HttpGet]
        public ActionResult Update(long id)
        {
            SetViewBag();
            SetUserName();
            var post = new ArticleDAO().GetArticleByID(id);
            return View(post);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Post post)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDAO();
                var result = dao.Update(post);
                if (result)
                {
                    SetAlert("Cập nhật bài viết thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi thông tin cập nhật");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var result = new ArticleDAO().Delete(id);
            if(result)
            {
                SetAlert("Bài viết của bạn đã được xóa", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa bài viết không thành công", "error");
            }
            return RedirectToAction("Index");
        }
    }
}