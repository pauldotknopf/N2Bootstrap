using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette.BundleProcessing;
using Cassette.Stylesheets;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class LessBundlePipelineModifier : IBundlePipelineModifier<StylesheetBundle>
    {
        public IBundlePipeline<StylesheetBundle> Modify(IBundlePipeline<StylesheetBundle> pipeline)
        {
            var index = pipeline.IndexOf<ParseCssReferences>();
            pipeline.Insert<LessBundleProcessor>(index + 1);

            return pipeline;
        }
    }
}
