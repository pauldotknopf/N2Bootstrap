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
            string path = HostingEnvironment.MapPath(Url.ResolveTokens(Url.ThemesUrlToken));
            if (Directory.Exists(path))
            {
                var defaultThemePath = Path.Combine(path, "Default");
                foreach (string directoryPath in Directory.GetDirectories(path))
                {
                    string directoryName = Path.GetFileName(directoryPath);
                    if (directoryName != null && !directoryName.StartsWith("."))
                    {
                        bundles.Add<StylesheetBundle>(directoryName + "-css", new List<string>
                        {
                            GetThemedItem("content/less/bootstrap.less", directoryPath, defaultThemePath),
                            GetThemedItem("content/less/responsive.less", directoryPath, defaultThemePath),
                            GetThemedItem("content/custom.less", directoryPath, defaultThemePath)
                        });
                        bundles.Add<ScriptBundle>(directoryName + "-js", new List<string>
                        {
                            GetThemedItem("scripts/jquery-1.8.2.js", directoryPath, defaultThemePath),
                            GetThemedItem("scripts/bootstrap.js", directoryPath, defaultThemePath)
                        });
                    }
                }
            }
        }

        private string GetThemedItem(string content, string themePath, string defaultThemePath)
        {
            var themed = Path.Combine(themePath, content);
            return File.Exists(themed) ? themed : Path.Combine(defaultThemePath, content);
        }
    }
}