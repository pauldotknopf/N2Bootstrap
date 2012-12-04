using System.Collections.Generic;
using System.IO;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace N2Bootstrap.Library.Cassette
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.Add<StylesheetBundle>("managementcss", new List<string>
                                                     {
                                                         GetContent("content/css/others/boilerplate.css"),
                                                         GetContent("content/css/others/jquery-layout.css"),
                                                         GetContent("content/css/others/jquery-ui-1.8.18.css"),
                                                         GetContent("content/css/site.less")
                                                     });

            bundles.Add<ScriptBundle>("managementjs", new List<string>
                                                     {
                                                         GetContent("scripts/jquery-1.7.1.js"),
                                                         GetContent("scripts/jquery-ui-1.8.18.js"),
                                                         GetContent("scripts/modernizr-2.5.3.js"),
                                                         GetContent("scripts/jquery.validate.js"),
                                                         GetContent("scripts/jquery.validate.unobtrusive.js"),
                                                         GetContent("scripts/jquery.unobtrusive-ajax.js"),
                                                         GetContent("scripts/jquery.layout-1.3.0.rc30.4.js"),
                                                         GetContent("scripts/jquery.blockUI.js"),
                                                         GetContent("scripts/jquery.form.js"),
                                                         GetContent("scripts/tiny_mce/jquery.tinymce.js"),
                                                         GetContent("scripts/Kendo/kendo.web.min.js"),
                                                         GetContent("scripts/Kendo/kendo.aspnetmvc.min.js"),
                                                         GetContent("scripts/method.bootstrap.js"),
                                                         GetContent("scripts/method.management.js")
                                                     });
        }

        /// <summary>
        /// Pass it in a path like /content/css/site.css
        /// If the path doesn't exist, it will return /method/content/css/site.css.
        /// This way, you can override css without modifiying the ~/Method/ directory directly for upgrading.
        /// </summary>
        /// <param name="path"></param>
        private string GetContent(string path)
        {
            var absolutePath = path.StartsWith("~/") ? path : "~/" + path;

            return File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(absolutePath))
                       ? absolutePath.Substring(2, absolutePath.Length - 2)
                       : "method/" + absolutePath.Substring(2, absolutePath.Length - 2);
        }
    }
}
