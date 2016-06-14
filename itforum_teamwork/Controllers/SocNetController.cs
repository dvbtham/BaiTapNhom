using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class SocNetController : Controller
    {
        public ActionResult Index(long id)
        {
            var socnetDAO = new SocNetDAO().MySocNet(id);
            return View(socnetDAO);
        }
    }
}