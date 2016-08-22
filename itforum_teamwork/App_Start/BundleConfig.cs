using System.Web;
using System.Web.Optimization;

namespace itforum_teamwork
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //js client
            bundles.Add(new ScriptBundle("~/bundles/jscore").Include(
                     "~/Content/Client/js/jquery.js",
                     "~/Content/Client/js/bootstrap.min.js",
                     "~/Scripts/jquery.unobtrusive-ajax.js",
                     "~/Content/Client/js/jquery.scrollUp.min.js",
                     "~/Content/Client/js/price-range.js",
                     "~/Content/Client/plugins/ckeditor/ckeditor.js",
                     "~/Content/Client/plugins/ckfinder/ckfinder.js",
                     "~/Content/Client/js/jquery.prettyPhoto.js",
                     "~/Content/Client/js/main.js",
                     "~/Content/Client/js/alertstyle.js"));
            //css client
            bundles.Add(new StyleBundle("~/bundles/csscore").Include(
                      "~/Content/Client/css/bootstrap.min.css",
                      "~/Content/Client/css/font-awesome.min.css",
                      "~/Content/Client/css/prettyPhoto.css",
                      "~/Content/Client/css/animate.css",
                      "~/Content/Client/css/main.css",
                      "~/Content/Client/css/responsive.css"));

            //js admin
            bundles.Add(new ScriptBundle("~/bundles/jsadmin").Include(
                     "~/Scripts/jquery-1.10.2.min.js",
                     "~/Content/Client/js/bootstrap.min.js",
                     "~/Scripts/jquery.validate.js",
                     "~/scripts/jquery.validate.unobtrusive.js",
                     "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                     "~/Scripts/jquery.unobtrusive-ajax.js",
                     "~/Content/Admin/plugins/ckfinder/ckfinder.js",
                     "~/Content/Admin/plugins/ckeditor/ckeditor.js",
                     "~/Content/Admin/bower_components/metisMenu/dist/metisMenu.min.js",
                     "~/Content/Admin/dist/js/sb-admin-2.js",
                     "~/Content/Client/js/alertstyle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsadminController").Include(
                     "~/Content/Admin/js/jsControllers/userController.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jsHubCounter").Include(
            //         "~/Scripts/jquery.signalR-2.2.0.min.js",
            //         "~/signalr/hubs"));

            bundles.Add(new ScriptBundle("~/bundles/jsdataTable").Include(
                     "~/Scripts/DataTables/jquery.dataTables.min.js"));

            //css admin
            bundles.Add(new StyleBundle("~/bundles/cssadmin").Include(
                      "~/Content/Admin/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/Content/Admin/bower_components/bootstrap/dist/css/bootstrap-responsive.css",
                       "~/Content/Admin/bower_components/bootstrap/dist/css/icon-bootstrap.css",
                      "~/Content/Admin/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/Content/Admin/dist/css/sb-admin-2.css",
                      "~/Content/Admin/bower_components/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/cssdataTable").Include(
                      "~/Content/DataTables/css/jquery.dataTables.min.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
