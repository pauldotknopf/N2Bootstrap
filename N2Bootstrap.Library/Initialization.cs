using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;
using N2.Engine;
using N2.Plugin;

namespace N2Bootstrap.Library
{
    [Service]
    public class Initialization : IAutoStart
    {
        public void Start()
        {
            var bundles = BundleTable.Bundles;

            var stylesBundle = new Bundle("~/managementcss");
            stylesBundle.Include("~/bootstrap/themes/default/content/bootstrap/bootstrap.less");
            stylesBundle.Transforms.Add(new CssTransformer());
            bundles.Add(stylesBundle);
        }

        public void Stop()
        {
        }
    }
}
