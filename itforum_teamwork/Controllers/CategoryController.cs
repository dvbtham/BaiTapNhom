using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class CategoryController : Controller
    {
        public PartialViewResult PopCategory()
        {
            var list = new CategoryDAO().SortedList();
            return PartialView(list);
        }
        public PartialViewResult SubMenu()
        {
            var list = new CategoryDAO().SubMenuList();
            return PartialView(list);
        }
        public PartialViewResult AssetsCategory()
        {
            var list = new CategoryDAO().SortedAssetsList();
            return PartialView(list);
        }
    }
}