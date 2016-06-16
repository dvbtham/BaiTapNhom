using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDAO
    {
        itforumEntities db = null;
        public CategoryDAO()
        {
            db = new itforumEntities();
        }
        public List<Category> ListAll()
        {
            return db.Categories.OrderBy(x => x.CategoryName).Where(x => x.Status == true).ToList();
        }
        public List<Category> SortedList()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.CategoryID).ToList();
        }
        public List<Category> SubMenuList()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.CategoryName).ToList();
        }
        public List<AssetType> SortedAssetsList()
        {
            return db.AssetTypes.OrderBy(x => x.AssetTypeID).ToList();
        }

        public Category GetCategoryByID(int? id)
        {
            return db.Categories.SingleOrDefault(x => x.CategoryID == id);
        }
        public int GetItemNumber(long id)
        {
            var post = db.Posts.Count(x => x.CategoryID == id);
            return post;
        }
        public int GetItemNumber()
        {
            var post = db.Posts.Count();
            return post;
        }
    }
}
