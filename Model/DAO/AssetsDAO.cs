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
    }
}
