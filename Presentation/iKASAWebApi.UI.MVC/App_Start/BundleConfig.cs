using System.Web;
using System.Web.Optimization;

namespace KMUH.iKASAWebApi.UI.MVC
{
    public class BundleConfig
    {
        static string strScriptServerAddress = System.Configuration.ConfigurationManager.AppSettings["ScriptServerAddress"];
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery", strScriptServerAddress + "/Scripts/jquery.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval", strScriptServerAddress + "/Scripts/jqueryval_bundles.js").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr", strScriptServerAddress + "/Scripts/modernizr-2.6.2.js").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap", strScriptServerAddress + "/Scripts/bootstrap_bundles.js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap_css", strScriptServerAddress + "/Content/bootstrap.min.css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                      "~/Scripts/Toolbar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/Toolbar.css",
                      "~/Content/Templatecss.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css",
            //          "~/Content/Toolbar.css",
            //          "~/Content/Templatecss.css"));


        }
    }
}