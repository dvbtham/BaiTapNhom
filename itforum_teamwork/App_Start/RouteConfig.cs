using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace itforum_teamwork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Bài viết
            routes.MapRoute(
                    "Doc Bai Viet",
                    "bai-viet/chi-tiet/{metatile}-{id}",
                    new { controller = "Article", action = "Details", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            routes.MapRoute(
                    "Tat Ca Bai Viet",
                    "bai-viet",
                    new { controller = "Article", action = "Home", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            routes.MapRoute(
                    "Bai Viet Theo Tac Gia",
                    "bai-viet/tac-gia/{metatile}-{id}",
                    new { controller = "Article", action = "PostByUser", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            routes.MapRoute(
                    "Bai Viet Theo Tags",
                    "bai-viet/tag/{metatile}-{id}",
                    new { controller = "Article", action = "Tag", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            routes.MapRoute(
                    "Bai Viet Theo Danh Muc",
                    "bai-viet/danh-muc/{metatile}-{id}",
                    new { controller = "Article", action = "Index", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            //Quản lý người dùng
            routes.MapRoute(
                    "thong tin nguoi dung",
                    "ho-so/thong-tin-nguoi-dung",
                    new { controller = "Manage", action = "Index", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            routes.MapRoute(
                    "chinh sua nguoi dung",
                    "ho-so/cap-nhat-{id}",
                    new { controller = "Manage", action = "Edit", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );
            
            //Quản lý bài viết
            routes.MapRoute(
                    "bai viet cua toi",
                    "bai-viet/bai-viet-cua-toi",
                    new { controller = "Post", action = "Index", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );

            //Liên hệ
            routes.MapRoute(
                   "Lien he",
                   "lien-he",
                   new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );

            //Đăng ký tài khoản
            routes.MapRoute(
                  "Dang ky",
                  "dang-ky",
                  new { controller = "Account", action = "Signup", id = UrlParameter.Optional },
                  new[] { "itforum_teamwork.Controllers" }
              );

            //Đăng nhập
            routes.MapRoute(
                  "Dang nhap",
                  "dang-nhap",
                  new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                  new[] { "itforum_teamwork.Controllers" }
              );
            //Kích hoạt tài khoản
            routes.MapRoute(
                   "Kich hoat",
                   "kich-hoat/thanh-cong/actived/{id}",
                   new { controller = "Confirm", action = "Actived", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );
            //Taif nguyeen
            routes.MapRoute(
                   "Xem tai nguyen",
                   "tai-nguyen/{metatitle}-{id}",
                   new { controller = "Assets", action = "Details", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );
            routes.MapRoute(
                   "Xem tai nguyen theo nguoi dang",
                   "tai-nguyen/nguoi-dang/{metatitle}-{id}",
                   new { controller = "Assets", action = "AssetByUser", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );
            routes.MapRoute(
                   "Xem tai nguyen theo danh muc",
                   "tai-nguyen/the-loai/{metatitle}-{id}",
                   new { controller = "Assets", action = "AssetByAssType", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );
            routes.MapRoute(
                   "Danh sach tai nguyen ",
                   "tai-nguyen",
                   new { controller = "Assets", action = "Index", id = UrlParameter.Optional },
                   new[] { "itforum_teamwork.Controllers" }
               );
            


            //Default
            routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id}",
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new[] { "itforum_teamwork.Controllers" }
                );
        }
    }
}
