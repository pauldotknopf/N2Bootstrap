using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;
using N2.Plugin;
using N2.Web;
using N2Bootstrap.Library.Cassette;
using System.Linq;
using N2Bootstrap.Library.Resources;

// default resources
[assembly: BootstrapResource("content/site.less", ResponsiveMode = BootstrapResourceAttribute.ResponsiveModeEnum.NotResponsive)]
[assembly: BootstrapResource("content/site-responsive.less", ResponsiveMode = BootstrapResourceAttribute.ResponsiveModeEnum.Responsive)]
[assembly: BootstrapResource("scripts/jquery-1.8.2.js", SortOrder = 0)]
[assembly: BootstrapResource("scripts/jquery.validate.js", SortOrder = 1)]
[assembly: BootstrapResource("scripts/jquery.validate.unobtrusive.js", SortOrder = 2)]
[assembly: BootstrapResource("scripts/jquery.validate.bootstrap.js", SortOrder = 3)]
[assembly: BootstrapResource("scripts/bootstrap.js", SortOrder = 4)]
[assembly: BootstrapResource("scripts/script.js", SortOrder = 5)]

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

                var regularCss = new List<string>();
                var responsiveCss = new List<string>();
                var javascript = new List<string>();

                var resourcesPlugins = N2.Context.Current.Resolve<IPluginFinder>().GetPlugins<BootstrapResourceAttribute>();

                foreach (var resourcesPlugin in resourcesPlugins)
                {
                    if (resourcesPlugin.ResourceType == BootstrapResourceAttribute.ResourceTypeEnum.CssOrLess)
                    {
                        switch (resourcesPlugin.ResponsiveMode)
                        {
                            case BootstrapResourceAttribute.ResponsiveModeEnum.Responsive:
                                responsiveCss.Add(resourcesPlugin.ThemedResourceLocation);
                                break;
                            case BootstrapResourceAttribute.ResponsiveModeEnum.NotResponsive:
                                regularCss.Add(resourcesPlugin.ThemedResourceLocation);
                                break;
                            case BootstrapResourceAttribute.ResponsiveModeEnum.Both:
                                responsiveCss.Add(resourcesPlugin.ThemedResourceLocation);
                                regularCss.Add(resourcesPlugin.ThemedResourceLocation);
                                break;
                        }
                    }
                    else
                    {
                        javascript.Add(resourcesPlugin.ThemedResourceLocation);
                    }
                }

                foreach (var directory in themeDirectories)
                {
                    string directoryName = Path.GetFileName(directory).ToLower();
                    if (directoryName != null && !directoryName.StartsWith("."))
                    {
                        bundles.Add<StylesheetBundle>(directoryName + "-css",
                            regularCss.Select(x => GetThemedItem(x, directory, defaultThemePath)),
                            (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));

                        bundles.Add<StylesheetBundle>(directoryName + "-responsive-css",
                            responsiveCss.Select(x => GetThemedItem(x, directory, defaultThemePath)),
                            (bundle) => bundle.HtmlAttributes.Add("data-theme", directoryName));

                        bundles.Add<ScriptBundle>(directoryName + "-js",
                            javascript.Select(x => GetThemedItem(x, directory, defaultThemePath)),
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