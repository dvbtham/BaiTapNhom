using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Configuration;
using Model.EF;
using itforum_teamwork.Models;
using System.Threading.Tasks;
using System.Net;

namespace itforum_teamwork.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Index(MailModel model)
        {
            if (ModelState.IsValid)
            {
                string ToEmail = ConfigurationManager.AppSettings["Username"].ToString();
                string fromEmail = model.FromEmail;
                MailAddress from = new MailAddress(fromEmail, model.FromName);
                MailAddress to = new MailAddress(ToEmail);

                var body = "<p>Tin nhắn từ: {0} ({1})</p><p>Nội dung:</p><p>{2}</p>";
                var message = new MailMessage(fromEmail,ToEmail);

                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = ConfigurationManager.AppSettings["Username"].ToString(),
                        Password = ConfigurationManager.AppSettings["Password"].ToString()
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }
        public ActionResult Sent()
        {
            return View();
        }
    }
}