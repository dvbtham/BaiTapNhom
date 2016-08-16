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
    public class PostController : BaseController
    {
        private const int defaultPageSize = 10;
        public ActionResult Index(string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            var session = (UserLogin)Session[itforum_teamwork.Common.CommonConstants.CLIENT_USER_SESSION];
            IList<Post> posts = new ArticleDAO().GetPostsByUserID(session.UserID);
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
                return PartialView("_AjaxPostList", posts);
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
                var session = (UserLogin)Session[itforum_teamwork.Common.CommonConstants.CLIENT_USER_SESSION];
                post.UserID = session.UserID;
                post.Content = postModel.Content;
                post.ShortContent = postModel.ShortContent;
                post.CategoryID = postModel.CategoryID;
                post.PostedDate = DateTime.Now;
                post.Status = true;
                post.Views = 0;
                if (!string.IsNullOrEmpty(postModel.Avatar))
                    post.Avatar = postModel.Avatar;
                else
                    post.Avatar = "/Data/images/ArticleImg/no_image.png";

                var result = dao.Insert(post);
                if (result > 0)
                {
                    SetAlert("Đăng bài viết thành công", "success");
                    return RedirectToAction("Index", "Post");
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
                    var session = (UserLogin)Session[itforum_teamwork.Common.CommonConstants.CLIENT_USER_SESSION];
                    return RedirectToAction("Index", "Post");
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
            new ArticleDAO().Delete(id);
            SetAlert("Bài viết đã xóa thành công", "success");
            return RedirectToAction("Index");
        }
    }
}