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
            public string ResolveRelativePath(string basePath, string relativePath)
            {
                return relativePath;
            }
        }

        class FileSystem : IVirtualFileSystemWrapper
        {
            public bool FileExists(string virtualPath)
            {
                throw new NotImplementedException();
            }

            public System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, string[] virtualPathDependencies, DateTime utcStart)
            {
                throw new NotImplementedException();
            }

            public string GetCacheKey(string virtualPath)
            {
                throw new NotImplementedException();
            }

            public byte[] GetFileBinaryContent(string virtualPath)
            {
                throw new NotImplementedException();
            }

            public System.IO.Stream GetFileStream(string virtualPath)
            {
                throw new NotImplementedException();
            }

            public string GetFileTextContent(string virtualPath)
            {
                throw new NotImplementedException();
            }

            public bool IsTextFile(string virtualPath, int sampleSize, out Encoding encoding)
            {
                throw new NotImplementedException();
            }

            public string ToAbsolutePath(string virtualPath)
            {
                throw new NotImplementedException();
            }
        }
    }
}
