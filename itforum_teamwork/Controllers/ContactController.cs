using itforum_teamwork.Common;
using itforum_teamwork.Models;
using Model.DAO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            CommonConstants.IsAjax = false;
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
                var message = new MailMessage(fromEmail, ToEmail);

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

        public PartialViewResult _ContactPartial()
        {
            var myInfo = new ContactDAO().GetContactInfo();
            return PartialView(myInfo);
        }

        public PartialViewResult TopContactInfo()
        {
            var myInfo = new ContactDAO().GetContactInfo();
            return PartialView(myInfo);
        }
    }
}