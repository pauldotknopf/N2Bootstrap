using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Optimization;
using BundleTransformer.Core;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using N2.Engine;
using N2.Plugin;
using N2.Web;

namespace N2Bootstrap.Library
{
    [Service]
    public class Initialization : IAutoStart
    {
        public void Start()
        {
            var bundles = BundleTable.Bundles;

            foreach (VirtualDirectory theme in HostingEnvironment.VirtualPathProvider.GetDirectory(Url.ResolveTokens(Url.ThemesUrlToken)).Children.Cast<object>().Where<object>(x => x is VirtualDirectory))
            {
                var stylesBundle = new Bundle("~/themed-styles-" + theme.Name.ToLower());
                stylesBundle.Include("~/bootstrap/themes/default/content/bootstrap/bootstrap.less");
                stylesBundle.Transforms.Add(new CssTransformer(
                    new NullMinifier(),
                    new List<ITranslator> { new Less.LessTranslater(theme.Name.ToLower()) },
                    new string[0],
                    new CoreSettings()));
                bundles.Add(stylesBundle);

                var responsiveStylesBundle = new Bundle("~/themed-styles-responsive-" + theme.Name.ToLower());
                responsiveStylesBundle.Include("~/bootstrap/themes/default/content/bootstrap/bootstrap.less");
                responsiveStylesBundle.Transforms.Add(new CssTransformer(
                    new NullMinifier(),
                    new List<ITranslator> { new Less.LessTranslater(theme.Name.ToLower()) },
                    new string[0],
                    new CoreSettings()));
                bundles.Add(responsiveStylesBundle);

                var scriptsBundle = new Bundle("~/themed-scripts-" + theme.Name.ToLower());
                scriptsBundle.Include("~/bootstrap/themes/default/content/bootstrap/bootstrap.less");
                scriptsBundle.Transforms.Add(new CssTransformer(
                    new NullMinifier(),
                    new List<ITranslator> { new Less.LessTranslater(theme.Name.ToLower()) },
                    new string[0],
                    new CoreSettings()));
                bundles.Add(scriptsBundle);
            }
        }

        public void Stop()
        {
        }
    }
}
