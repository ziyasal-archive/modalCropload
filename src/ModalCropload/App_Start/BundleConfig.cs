using System.Web.Optimization;

namespace ModalCropload
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Scripts/modernizr.custom.63321.js",
                "~/Scripts/jquery-1.10.0.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/placeholder.js",
                "~/Scripts/imagesloaded.pkgd.min.js",
                "~/Scripts/masonry.pkgd.min.js",
                "~/Scripts/jquery.swipebox.min.js",
                "~/Scripts/farbtastic/farbtastic.js",
                "~/Scripts/options.js",
                "~/Scripts/plugins.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/cssmain")
                .Include("~/Content/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/Site.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}
