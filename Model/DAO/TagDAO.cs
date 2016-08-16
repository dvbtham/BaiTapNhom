using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class TagDAO
    {
        itforumEntities db = null;
        public TagDAO()
        {
            db = new itforumEntities();
        }
        public int TagsCounter()
        {
            return db.Tags.Count();
        }
        public List<Tag> ListAll()
        {
            return db.Tags.ToList();
        }
       
        public IEnumerable<Tag> GetTagsPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Tag> tag = db.Tags;
            if (!string.IsNullOrEmpty(searchString))
            {
                tag = tag.Where(x => x.TagName.Contains(searchString));
            }
            return tag.OrderByDescending(x => x.TagID).ToPagedList(page, pageSize);
        }
        //Get tag
        public Tag ViewDetail(long id)
        {
            return db.Tags.Find(id);
        }
        //CRUD
        public bool AddTag(Tag tag)
        {
            try
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Tag tag)
        {
            try
            {
                var tagModel = db.Tags.Find(tag.TagID);
                tagModel.TagName = tag.TagName;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var tagModel = db.Tags.Include("PostTags").SingleOrDefault(x => x.TagID == id);
                db.Tags.Remove(tagModel);
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
