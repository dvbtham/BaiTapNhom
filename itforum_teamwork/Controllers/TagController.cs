using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class TagController : Controller
    {
        public PartialViewResult Index()
        {
            var model = new TagDAO().ListAll();
            return PartialView(model);
        }
    }
}