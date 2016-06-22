using itforum_teamwork.Areas.Admin.Models;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        public ActionResult Index()
        {
            var info = new ContactDAO().GetContactInfo();
            return View(info);
        }
        public ActionResult Create(ContactModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new ContactDAO();
                var contact = new Contact();
                contact.Email = model.Email;
                contact.Fax = model.Fax;
                contact.Phone = model.Phone;
                contact.Address = model.Address;

                var res = new ContactDAO().Create(contact);
                if(res)
                {
                    SetAlert("Thêm thông tin liên hệ thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm thông tin liên hệ thất bại", "error");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin không chính xác!.");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new ContactDAO().ViewDetails(id);
            return View(model);
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(Contact model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContactDAO();
                var result = dao.Edit(model);
                if (result)
                {
                    SetAlert("Cập nhật thông tin liên hệ thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Cập nhật thông tin cá nhân thất bại", "error");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin không chính xác!. ");
            }
            return View("Index");
        }
    }
}