using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Model.DAO
{
    public class ArticleDAO
    {
        itforumEntities db = null;
        public ArticleDAO()
        {
            db = new itforumEntities();
        }
        public int? AddViews(long id)
        {
            var post = db.Posts.Find(id);
            post.Views += 1;
            return post.Views;
        }
        public IEnumerable<Post> GetListOfArticlePagging(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString) || x.Detail.Contains(searchString) && x.Status == true);
            }
            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Post> GetArticlePaggingByUser(string searchString, long? id, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts.Where(x => x.UserID == id && x.Status == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString) || x.Detail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Post> GetListByCatID(long id, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            model = model.Where(x => x.CategoryID == id);

            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }
        //Danh sách 10 bài viết nổi bật
        public List<Post> GetArticleTop10()
        {
            return db.Posts.OrderByDescending(x => x.Views).Take(10).ToList();
        }
        //set name
        public List<Post> ListPostAll()
        {
            return db.Posts.ToList();
        }


        public List<Post> GetNewArticle()
        {
            return db.Posts.OrderByDescending(x => x.PostedDate).Take(10).ToList();
        }

        public long GetNextID()
        {
            var nextID = db.Posts.OrderBy(x => x.PostID).First();
            return nextID.PostID;
        }

        public List<Tag> ListTag(long PostID)
        {
            var model = (from a in db.Tags
                         join b in db.PostTags
                         on a.TagID equals b.TagID
                         where b.PostID == PostID
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             TagID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public IEnumerable<Post> ListAllByTag(long tagID, int page, int pageSize)
        {
            var model = (from a in db.Posts
                         join b in db.PostTags
                         on a.PostID equals b.PostID
                         where b.TagID == tagID
                         select new
                         {
                             Title = a.Title,
                             ImageShowOnHome = a.ImageShowOnHome,
                             Content = a.Content,
                             PostedDate = a.PostedDate,
                             UserID = a.UserID,
                             PostID = a.PostID,
                             CategoryID = a.CategoryID

                         }).AsEnumerable().Select(x => new Post()
                         {
                             Title = x.Title,
                             ImageShowOnHome = x.ImageShowOnHome,
                             Content = x.Content,
                             PostedDate = x.PostedDate,
                             UserID = x.UserID,
                             PostID = x.PostID,
                             CategoryID = x.CategoryID
                         });
            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }

        public Tag GetTag(long id)
        {
            return db.Tags.Find(id);
        }

        public List<Post> GetHotArticle()
        {
            return db.Posts.OrderByDescending(x => x.Views).Take(5).ToList();
        }


        public IEnumerable<Post> GetListByUserID(long id, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            model = model.Where(x => x.UserID == id);

            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Post> GetListPaging(int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            return model.OrderByDescending(x => x.PostedDate).ToPagedList(page, pageSize);
        }
        public Post GetArticleByID(long id)
        {
            return db.Posts.Find(id);
        }



        public Post GetNextArticleByID(long PostID, long CatID)
        {
            var currPost = db.Posts.Find(PostID);
            var nextPost = db.Posts.SingleOrDefault(x => x.PostedDate < currPost.PostedDate && x.CategoryID == CatID && x.PostID != PostID);

            return nextPost;
        }

        public long Insert(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return post.PostID;
        }
        public bool Update(Post post)
        {
            try
            {
                var postModel = db.Posts.Find(post.PostID);

                postModel.CategoryID = post.CategoryID;
                postModel.Detail = post.Detail;
                postModel.Content = post.Content;
                postModel.UserID = post.UserID;
                postModel.Title = post.Title;

                if (!string.IsNullOrEmpty(postModel.ImageShowOnHome))
                    postModel.ImageShowOnHome = post.ImageShowOnHome;
                else
                    post.ImageShowOnHome = "/Data/images/ArticleImg/no_image.png";

                postModel.PostedDate = post.PostedDate;
                postModel.Status = post.Status;
                postModel.Views = post.Views;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateViews(long id)
        {
            try
            {
                var post = db.Posts.Find(id);
                post.Views += 1;
                db.Entry(post);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var post = db.Posts
                    .Include(x=>x.PostTags)
                    .Include(x=>x.Pictures)
                    .Include(x => x.Videos)
                    .SingleOrDefault(x => x.PostID == id);

                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
