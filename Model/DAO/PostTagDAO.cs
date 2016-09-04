using Model.EF;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class PostTagDAO
    {
        private itforumEntities db = null;

        public PostTagDAO()
        {
            db = new itforumEntities();
        }

        public IEnumerable<PostTag> ListPostTagAll(int pageNumber, int pageSize)
        {
            var model = db.PostTags;
            return model.GroupBy(x => x.PostID).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.PostID).ToPagedList(pageNumber, pageSize);
        }

        public IEnumerable<PostTag> PostTagAll()
        {
            return db.PostTags.ToList();
        }

        public List<PostTag> GetTagByID(long postID)
        {
            return db.PostTags.Where(x => x.PostID == postID).ToList();
        }

        public Post GetPostTitle(long postID)
        {
            return db.Posts.Find(postID);
        }

        public Tag GetTagName(long tagID)
        {
            return db.Tags.Find(tagID);
        }

        //CRUD
        public int AddTag(PostTag tag)
        {
            try
            {
                bool isTagged = db.PostTags.Count(x => x.PostID == tag.PostID && x.TagID == tag.TagID) > 0;
                if (isTagged)
                {
                    return -1;
                }
                else
                {
                    db.PostTags.Add(tag);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch
            {
                return 2;
            }
        }

        public bool Edit(long postID, long tagID)
        {
            try
            {
                var tagModel = db.PostTags.SingleOrDefault(x => x.PostID == postID && x.TagID == tagID);
                db.PostTags.Attach(tagModel);
                var entry = db.Entry(tagModel);
                entry.Property(e => e.TagID).IsModified = true;
                entry.Property(e => e.TagID).IsModified = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long postID, long tagID)
        {
            try
            {
                var model = db.PostTags.SingleOrDefault(x => x.PostID == postID && x.TagID == tagID);
                db.PostTags.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Get tag
        public PostTag ViewDetail(long postID, long tagID)
        {
            return db.PostTags.SingleOrDefault(x => x.PostID == postID && x.TagID == tagID);
        }

        public List<Tag> ListTagName()
        {
            return db.Tags.ToList();
        }

        public List<Post> ListPostName()
        {
            return db.Posts.ToList();
        }
    }
}