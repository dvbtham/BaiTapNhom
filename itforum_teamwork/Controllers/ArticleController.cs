using itforum_teamwork.Models;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using MvcPaging;

namespace itforum_teamwork.Controllers
{
    public class ArticleController : Controller
    {
        private const int defaultPageSize = 10;
        public ActionResult Index(string searchString, int? page)
        {
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

        public ActionResult PostByUser(long id, int? page )
        {
            IList<Post> posts = new ArticleDAO().GetPostsByUserID(id);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            posts = posts.ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AjaxPostList", posts);
            }
            else
                return View(posts);
        }

        public ActionResult PostByCatID(long id, int? page)
        {
            IList<Post> posts = new ArticleDAO().GetPostsByCatID(id);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            posts = posts.ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AjaxPostList", posts);
            }
            else
                return View(posts);
        }

        public ActionResult Details(long id)
        {
            var viewModel = new ArticleDAO().GetArticleByID(id);
            ViewBag.Tags = new ArticleDAO().ListTag(id);
            if (viewModel == null)
                return RedirectToAction("Index", "Error");

            // next and last posts
            #region
            itforumEntities db = new itforumEntities();
            var lastID = db.Posts.OrderByDescending(s => s.PostID)
                .Where(x => x.CategoryID == viewModel.CategoryID)
                .FirstOrDefault().PostID;

            var firstID = db.Posts.OrderBy(s => s.PostID)
                .Where(x => x.CategoryID == viewModel.CategoryID)
                .FirstOrDefault().PostID;

            if (lastID != id)
            {
                var nextID = db.Posts.Where(x => x.CategoryID == viewModel.CategoryID)
                .FirstOrDefault(i => i.PostID > viewModel.PostID).PostID;

                var viewModelNext = new ArticleDAO().GetArticleByID(nextID);

                if (nextID != 0)
                {
                    ViewBag.NextID = nextID;

                    ViewBag.nextTitle = viewModelNext.Title;

                }
                else
                {
                    return View();
                }

                //Kiem tra null hay khong
            }
            else
            {
                ViewBag.NextID = viewModel.PostID;
                ViewBag.nextTitle = viewModel.Title;
            }

            if (firstID != id)
            {
                var prevArt = db.Posts.Where(x => x.CategoryID == viewModel.CategoryID && x.PostID < viewModel.PostID)
                    .OrderByDescending(x => x.PostID)
                    .FirstOrDefault();

                var viewModelPrev = new ArticleDAO().GetArticleByID(prevArt.PostID);

                if (prevArt != null)
                {
                    ViewBag.PrevID = prevArt.PostID;
                    ViewBag.prevTitle = prevArt.Title;
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.PrevID = viewModel.PostID;
                ViewBag.prevTitle = viewModel.Title;
            }

            #endregion

            //dem so luot xem bai viet

            IPAddressModel myIP = CheckCookie();
            if (myIP == null)
            {
                new ArticleDAO().UpdateViews(id);
                AddIPCookies(id);
            }
            else
            {
                string postID = myIP.PostID;
                AddIPCookies(id);
                myIP = CheckCookie();
                if (myIP.IP != GetIDAddress.myIPAddress() || myIP.PostID != id.ToString())
                {
                    if (postID != id.ToString())
                    {
                        new ArticleDAO().UpdateViews(id);
                    }
                }
            }
            var myPost = new ArticleDAO().GetArticleByID(id);
            return View(myPost);

        }

        public ActionResult Tag(long id, int page = 1, int pageSize = 10)
        {
            var model = new ArticleDAO().ListAllByTag(id, page, pageSize);

            ViewBag.Tag = new ArticleDAO().GetTag(id);
            return View(model);
        }
        public PartialViewResult Top10Article()
        {
            var top10 = new ArticleDAO().GetArticleTop10();
            ViewBag.NewPost = new ArticleDAO().GetNewArticle();
            return PartialView(top10);
        }

        //kiem tra cookie ip
        public IPAddressModel CheckCookie()
        {
            IPAddressModel myIP = null;
            string ip = string.Empty, postID = string.Empty;

            if (Request.Cookies["client_ip"] != null)
                ip = Request.Cookies["client_ip"].Value;

            if (Request.Cookies["client_postid"] != null)
                postID = Request.Cookies["client_postid"].Value;

            if (!string.IsNullOrEmpty(ip))
                myIP = new IPAddressModel { IP = ip, PostID = postID };

            return myIP;
        }

        public void AddIPCookies(long id)
        {
            HttpCookie ckIP = new HttpCookie("client_ip");
            ckIP.Expires = DateTime.Now.AddHours(1);
            ckIP.Value = GetIDAddress.myIPAddress();
            Response.Cookies.Add(ckIP);

            HttpCookie ckPostID = new HttpCookie("client_postid");
            ckPostID.Expires = DateTime.Now.AddHours(1);
            ckPostID.Value = id.ToString();
            Response.Cookies.Add(ckPostID);
        }
    }
}