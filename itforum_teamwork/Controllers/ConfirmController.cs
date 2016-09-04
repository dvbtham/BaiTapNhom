using Model.DAO;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace itforum_teamwork.Controllers
{
    public class ConfirmController : Controller
    {
        public static string email = "";

        public async Task<ActionResult> Index()
        {
            string fromEmail = Session["client_Email"].ToString();
            email = fromEmail;
            string ToEmail = fromEmail;
            ViewBag.ActiveEmail = email;

            var user = new UserDAO().GetUserByEmail(Session["client_Email"].ToString());
            MailAddress from = new MailAddress(fromEmail, user.Name);
            MailAddress to = new MailAddress(ToEmail);

            var body = "<p>Tin nhắn từ: {0} ({1})</p><p>Nội dung:</p><p>{2}</p>";
            var message = new MailMessage(fromEmail, ToEmail);
            Random random = new Random();
            int id = random.Next(11111111, 88888888);
            message.Subject = "Kích hoạt tài khoản IT-Forum";
            message.Body = string.Format(body, user.Name, fromEmail, "<p>Chào mừng bạn đã đăng ký thành viên trên diễn đàn IT-Forum, "
            + "Để kích hoạt tài khoản </p><a href=" + "http://www.codto.somee.com" + "/kich-hoat/" + "thanh-cong/actived/" + id + "" + ">Click vào đây để kích hoạt</a>");
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
                return View();
            }
        }

        public ActionResult Actived()
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = new UserDAO().GetUserByEmail(email);
                var active = new UserDAO().Actived(user);
                if (active)
                {
                    return View();
                }
                else
                    return RedirectToAction("Index", "Error");
            }
            else
                return RedirectToAction("Index", "Error");
        }
    }
}