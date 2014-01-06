using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BundleTransformer.Core;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.Translators;
using BundleTransformer.Less;

namespace N2Bootstrap.Library.Less
{
    public class LessTranslater : TranslatorWithNativeMinificationBase
    {
        private readonly BundleTransformer.Less.Translators.LessTranslator _translater;

        public LessTranslater(string theme)
        {
            _translater = new BundleTransformer.Less.Translators.LessTranslator(null, BundleTransformerContext.Current.GetVirtualFileSystemWrapper(), new PathResolver(theme, BundleTransformerContext.Current.GetVirtualFileSystemWrapper()), BundleTransformerContext.Current.GetLessConfiguration());
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
            private readonly string _theme;
            private readonly IVirtualFileSystemWrapper _virtualFileSystemWrapper;
            private IRelativePathResolver _inner;

            public PathResolver(string theme, IVirtualFileSystemWrapper virtualFileSystemWrapper)
            {
                _theme = theme;
                _virtualFileSystemWrapper = virtualFileSystemWrapper;
                _inner = new CommonRelativePathResolver(BundleTransformerContext.Current.GetVirtualFileSystemWrapper());
            }

            public string ResolveRelativePath(string basePath, string relativePath)
            {
                var result = _inner.ResolveRelativePath(basePath, relativePath);

                var regex = new Regex(@"/bootstrap/themes/[\w\-. ]+/", RegexOptions.IgnoreCase);
                
                if (regex.IsMatch(result))
                {
                    // this resource is themeable
                    var themedLocation = regex.Replace(result, s => string.Format(@"/bootstrap/themes/{0}/", _theme));
                    var defaultLocation = regex.Replace(result, s => @"/bootstrap/themes/default/");
                    if (_virtualFileSystemWrapper.FileExists(themedLocation))
                        return themedLocation;
                    return defaultLocation;
                }

                return result;
            }
        }
    }
}
