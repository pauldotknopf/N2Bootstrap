using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BundleTransformer.Core;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.Translators;
using BundleTransformer.Less;

namespace N2Bootstrap.Library.Less
{
    public class LessTranslater : TranslatorWithNativeMinificationBase
    {
        private BundleTransformer.Less.Translators.LessTranslator _translater;

        public LessTranslater()
        {
            _translater = new BundleTransformer.Less.Translators.LessTranslator(null, BundleTransformerContext.Current.GetVirtualFileSystemWrapper(), new PathResolver(), BundleTransformerContext.Current.GetLessConfiguration());
        }

        public override IList<IAsset> Translate(IList<IAsset> assets)
        {
            return _translater.Translate(assets);
        }

        public override IAsset Translate(IAsset asset)
        {
            return _translater.Translate(asset);
        }

        class PathResolver : IRelativePathResolver
        {
            private IRelativePathResolver _inner;

            public PathResolver()
            {
                _inner = new CommonRelativePathResolver(BundleTransformerContext.Current.GetVirtualFileSystemWrapper());
            }

            public string ResolveRelativePath(string basePath, string relativePath)
            {
                var result = _inner.ResolveRelativePath(basePath, relativePath);
                return result;
            }
        }
    }
}
