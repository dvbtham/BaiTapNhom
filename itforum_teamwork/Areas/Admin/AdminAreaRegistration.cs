using System.Web.Mvc;

namespace itforum_teamwork.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //PostTag
            context.MapRoute(
                "Admin_PostTag_Delete",
                "admin/tag-bai-viet/xoa-tag/{postID}/{tagID}",
                new { controller = "PostTag", action = "Delete", id = UrlParameter.Optional },
                new[] { "itforum_teamwork.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_PostTag_Edit",
                "admin/tag-bai-viet/sua-tag/{postID}/{tagID}",
                new { controller = "PostTag", action = "Edit", id = UrlParameter.Optional },
                new[] { "itforum_teamwork.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_PostTag_Add",
                "admin/tag-bai-viet/them-tag",
                new { controller = "PostTag", action = "AddPostTag", id = UrlParameter.Optional },
                new[] { "itforum_teamwork.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "itforum_teamwork.Areas.Admin.Controllers" }
            );
        }
    }
}