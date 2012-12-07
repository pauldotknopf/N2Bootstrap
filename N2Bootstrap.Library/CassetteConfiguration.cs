using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;
using N2.Web;
using N2Bootstrap.Library.Cassette;
using System.Linq;
namespace N2Bootstrap.Library
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            N2.Context.Initialize(false);
            string path = Url.ResolveTokens(Url.ThemesUrlToken);
            if (HostingEnvironment.VirtualPathProvider.DirectoryExists(path))
            {
                var defaultThemePath = Path.Combine(path, "Default");

                var themeDirectories = new List<string>();
                foreach (VirtualDirectory virtualDirectory in HostingEnvironment.VirtualPathProvider.GetDirectory(path).Directories)
                {
                    var directory = virtualDirectory.VirtualPath.Replace("\\\\", "\\").Replace("\\", "/");
                    themeDirectories.Add(virtualDirectory.VirtualPath.EndsWith("/") ? directory.TrimEnd(Convert.ToChar("/")) : directory);
                }

                // if we are using vpp, the default directory doesn't return. It is a problem in N2.Packaging and this is the workaround.
                if (!themeDirectories.Any(x => Path.GetFileName(x).ToLower() == "default"))
                {
                    themeDirectories.Add("/Bootstrap/Themes/Default");
                }

                foreach (var directory in themeDirectories)
                {
                    string directoryName = Path.GetFileName(directory).ToLower();
                    if (directoryName != null && !directoryName.StartsWith("."))
                    {
                        // regular css
                        bundles.Add<StylesheetBundle>(directoryName + "-css", new List<string>
                        {
                            GetThemedItem("content/site.less", directory, defaultThemePath)
                        },
                        (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));

                        // responsive css
                        bundles.Add<StylesheetBundle>(directoryName + "-responsive-css", new List<string>
                        {
                            GetThemedItem("content/site-responsive.less", directory, defaultThemePath)
                        },
                        (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));

                        // scripts
                        bundles.Add<ScriptBundle>(directoryName + "-js", new List<string>
                        {
                            GetThemedItem("scripts/jquery-1.8.2.js", directory, defaultThemePath),
                            GetThemedItem("scripts/bootstrap.js", directory, defaultThemePath)
                        },
                        (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));
                    }
                }
            }
        }

        private string GetThemedItem(string content, string themePath, string defaultThemePath)
        {
            var themed = Path.Combine(themePath, content).Replace("\\\\", "\\").Replace("\\", "/");
            themed = HostingEnvironment.VirtualPathProvider.FileExists(themed) ? themed : Path.Combine(defaultThemePath, content).Replace("\\\\", "\\").Replace("\\", "/");
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