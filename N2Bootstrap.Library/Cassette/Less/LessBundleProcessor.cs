using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette;
using Cassette.BundleProcessing;
using Cassette.Stylesheets;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class LessBundleProcessor : IBundleProcessor<StylesheetBundle>
    {
        private readonly CassetteSettings _settings;

        public LessBundleProcessor(CassetteSettings settings)
        {
            _settings = settings;
        }
        
        public void Process(StylesheetBundle bundle)
        {
            string theme = string.Empty;
            if (bundle.HtmlAttributes.ContainsAttribute("data-theme"))
                theme = bundle.HtmlAttributes["data-theme"];

            foreach (var asset in bundle.Assets)
            {
                if (asset.Path.EndsWith(".less", StringComparison.OrdinalIgnoreCase))
                {
                    asset.AddAssetTransformer(new LessCompileAsset(new CassetteLessCompiler(theme), _settings.SourceDirectory));
                }
            }
        }
    }
}
