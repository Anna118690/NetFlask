using System.Web;
using System.Web.Optimization;

namespace NetFlask
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
             

            bundles.Add(new StyleBundle("~/Content/Css")
                .Include("~/Content/bootstrap.css",
                "~/css/style.css"));
            bundles.Add(new StyleBundle("~/Content/Pop")
                .Include("~/css/popuo-box.css"));
            bundles.Add(new ScriptBundle("~/Scripts/Pop")
                .Include("~/js/jquery.magnific-popup.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery")
                .Include("~/js/jquery.min.js")
                );


            bundles.Add(new ScriptBundle("~/Scripts/Flexi1")
                .Include("~/js/FlexiselDemo1.js")
                );
            bundles.Add(new ScriptBundle("~/Scripts/Flexi2")
               .Include("~/js/FlexiselDemo2.js")
               );
            bundles.Add(new ScriptBundle("~/Scripts/Flexisel").Include
                (
                "~/js/jquery.flexisel.js"
                ));
        }
    }
}
