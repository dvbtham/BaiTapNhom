using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class AssetsDAO
    {
        itforumEntities db = null;
        public AssetsDAO()
        {
            db = new itforumEntities();
        }

        public List<Asset> ListAll()
        {
            return db.Assets.ToList();
        }
        public Asset ViewDetail(int id)
        {
            return db.Assets.Find(id);
        }
        public List<AssetType> ListAllAssetType()
        {
            return db.AssetTypes.ToList();
        }
        public AssetType GetAssetTypeByID(int id)
        {
            return db.AssetTypes.Find(id);
        }
        public List<Asset> ByAssetTypeID(int id)
        {
            return db.Assets.Where(x => x.AssetTypeID == id).ToList();
        }
        public List<Asset> ByUserID(long id)
        {
            return db.Assets.Where(x => x.UserID == id).ToList();
        }
        //CRUD

        public bool Create(Asset model)
        {
            try
            {
                db.Assets.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(Asset model)
        {
            try
            {
                var myModel = db.Assets.Find(model.AssetID);
                myModel.Title = model.Title;
                myModel.Image = model.Image;
                myModel.Link = model.Link;
                myModel.AssetTypeID = model.AssetTypeID;
                myModel.LinkDuPhong = model.LinkDuPhong;
                myModel.EditedDate = DateTime.Now;
                db.Entry(myModel);
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
                var model = db.Assets.Find(id);
                db.Assets.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<AssetType> ListType()
        {
            return db.AssetTypes.ToList();
        }
    }
}
