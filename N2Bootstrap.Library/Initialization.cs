using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using N2.Engine;
using N2.Plugin;
using N2.Web;
using N2Bootstrap.Library.Resources;

// default resources
[assembly: BootstrapResource("content/site.less")]
[assembly: BootstrapResource("scripts/jquery-1.10.2.js", SortOrder = 0)]
[assembly: BootstrapResource("scripts/jquery.validate.js", SortOrder = 1)]
[assembly: BootstrapResource("scripts/jquery.validate.unobtrusive.js", SortOrder = 2)]
[assembly: BootstrapResource("scripts/jquery.validate.bootstrap.js", SortOrder = 3)]
[assembly: BootstrapResource("scripts/jquery.form.js", SortOrder = 4)]
[assembly: BootstrapResource("scripts/jquery.tweet.js", SortOrder = 5)]
[assembly: BootstrapResource("scripts/bootstrap.js", SortOrder = 6)]
[assembly: BootstrapResource("scripts/script.js", SortOrder = 7)]

namespace N2Bootstrap.Library
{
    [Service]
    public class Initialization : IAutoStart
    {
        private readonly IPluginFinder _pluginFinder;

        public Initialization(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }

        public void Start()
        {
            var bundles = BundleTable.Bundles;
            var regularCss = new List<string>();
            var javascript = new List<string>();
            var resourcesPlugins = _pluginFinder.GetPlugins<BootstrapResourceAttribute>();
            var themeDirectories = (from VirtualDirectory theme in HostingEnvironment.VirtualPathProvider.GetDirectory(Url.ResolveTokens(Url.ThemesUrlToken)).Directories select theme.VirtualPath).ToList();
            var defaultThemeDirectory = Path.Combine(Url.ResolveTokens(Url.ThemesUrlToken), "default");

            foreach (var resourcesPlugin in resourcesPlugins)
            {
                if (resourcesPlugin.ResourceType == BootstrapResourceAttribute.ResourceTypeEnum.CssOrLess)
                {
                    regularCss.Add(resourcesPlugin.ThemedResourceLocation);
                }
                else
                {
                    javascript.Add(resourcesPlugin.ThemedResourceLocation);
                }
            }

            foreach (var themeDirectory in themeDirectories)
            {
                var themeName = HostingEnvironment.VirtualPathProvider.GetDirectory(themeDirectory).Name.ToLower();
                
                var stylesBundle = new Bundle("~/themed-styles-" + themeName);
                foreach (var css in regularCss.Select(x => GetThemedItem(x, themeDirectory, defaultThemeDirectory)).Where((x => HostingEnvironment.VirtualPathProvider.FileExists(x))))
                    stylesBundle.Include(css);
                stylesBundle.Transforms.Add(new CssTransformer(
                    new NullMinifier(),
                    new List<ITranslator> { new Less.LessTranslater(themeName) },
                    new string[0],
                    new CoreSettings()));
                bundles.Add(stylesBundle);

                var scriptsBundle = new Bundle("~/themed-scripts-" + themeName);
                foreach (var js in javascript.Select(x => GetThemedItem(x, themeDirectory, defaultThemeDirectory)).Where((x => HostingEnvironment.VirtualPathProvider.FileExists(x))))
                    scriptsBundle.Include(js);
                scriptsBundle.Transforms.Add(new JsTransformer());
                bundles.Add(scriptsBundle);
            }
        }

        private string GetThemedItem(string content, string themePath, string defaultThemePath)
        {
            var themed = Path.Combine(themePath, content).Replace("\\\\", "\\").Replace("\\", "/");
            themed = HostingEnvironment.VirtualPathProvider.FileExists(themed) ? themed : Path.Combine(defaultThemePath, content).Replace("\\\\", "\\").Replace("\\", "/");
            return "~/" + themed;
        }

        public void Stop()
        {
        }
    }
}
