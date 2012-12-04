using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;
using N2.Web;

namespace N2Bootstrap.Library
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            string path = Url.ResolveTokens(Url.ThemesUrlToken);
            if (HostingEnvironment.VirtualPathProvider.DirectoryExists(path))
            {
                var defaultThemePath = Path.Combine(path, "Default");
                foreach (VirtualDirectory directory in HostingEnvironment.VirtualPathProvider.GetDirectory(path).Directories)
                {
                    string directoryName = directory.Name;
                    if (directoryName != null && !directoryName.StartsWith("."))
                    {
                        bundles.Add<StylesheetBundle>(directoryName + "-css", new List<string>
                        {
                            GetThemedItem("content/less/bootstrap.less", directory.VirtualPath, defaultThemePath),
                            GetThemedItem("content/less/responsive.less", directory.VirtualPath, defaultThemePath),
                            GetThemedItem("content/custom.less", directory.VirtualPath, defaultThemePath)
                        },
                        (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));
                        bundles.Add<ScriptBundle>(directoryName + "-js", new List<string>
                        {
                            GetThemedItem("scripts/jquery-1.8.2.js", directory.VirtualPath, defaultThemePath),
                            GetThemedItem("scripts/bootstrap.js", directory.VirtualPath, defaultThemePath)
                        },
                        (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));
                    }
                }
            }
        }

        private string GetThemedItem(string content, string themePath, string defaultThemePath)
        {
            var themed = Path.Combine(themePath, content);
            themed = HostingEnvironment.VirtualPathProvider.FileExists(themed) ? themed : Path.Combine(defaultThemePath, content);
            return themed.Substring(1);
        }

        public class BundleHtmlRender<T> : IBundleHtmlRenderer<T> where T : Bundle
        {
            public string Render(T bundle)
            {
                return "test";
            }
        }
    }
}