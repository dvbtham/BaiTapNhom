using itforum_teamwork.Common;
using Model.EF;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class ArticleDAO
    {
        private itforumEntities db = null;

        public ArticleDAO()
        {
            db = new itforumEntities();
        }

        public long? AddViews(long id)
        {
            var post = db.Posts.Find(id);
            post.Views += 1;
            return post.Views;
        }

        public List<Post> GetPosts()
        {
            return db.Posts.Where(x => x.Status == true).OrderByDescending(x => x.PostedDate).ToList();
        }

        public List<Post> GetPostsByUserID(long id)
        {
            return db.Posts.Where(x => x.UserID == id).OrderByDescending(x => x.PostedDate).ToList();
        }

        public List<Post> GetPostsByCatID(long id)
        {
            return db.Posts.Where(x => x.CategoryID == id).ToList();
        }

        //Danh sách 10 bài viết nổi bật
        public List<Post> GetArticleTop10()
        {
            return db.Posts.Where(x => x.Status == true).OrderByDescending(x => x.Views).Take(10).ToList();
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
                             Name = a.TagName
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             TagID = x.ID,
                             TagName = x.Name
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
                             ImageShowOnHome = a.Avatar,
                             Content = a.Content,
                             PostedDate = a.PostedDate,
                             UserID = a.UserID,
                             PostID = a.PostID,
                             CategoryID = a.CategoryID
                         }).AsEnumerable().Select(x => new Post()
                         {
                             Title = x.Title,
                             Avatar = x.ImageShowOnHome,
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
            return db.Posts.Where(x => x.Status == true).OrderByDescending(x => x.Views).Take(5).ToList();
        }

        public List<Post> GetListByUserID(long id)
        {
            return db.Posts.Where(x => x.UserID == id).OrderByDescending(x => x.PostedDate).ToList();
        }

        public IEnumerable<Post> GetListPaging(int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts.Where(x => x.Status == true);
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
                ShowError.ErrorMessage(dbEx);
            }
            return post.PostID;
        }

        public bool Update(Post post)
        {
            try
            {
                var postModel = db.Posts.Find(post.PostID);

                postModel.CategoryID = post.CategoryID;
                postModel.ShortContent = post.ShortContent;
                postModel.Content = post.Content;
                postModel.UserID = post.UserID;
                postModel.Title = post.Title;

                if (!string.IsNullOrEmpty(postModel.Avatar))
                    postModel.Avatar = post.Avatar;
                else
                    post.Avatar = "/Data/images/ArticleImg/no_image.png";
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
                    .Include("PostTags")
                    .Include("User")
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