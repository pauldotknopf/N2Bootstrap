using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cassette;
using N2.Web;

namespace N2Bootstrap.Library.Cassette
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly string _cassetteHandlerPrefix;
        private static Type _internalUrlGenerator = Type.GetType("Cassette.UrlGenerator, Cassette");
        private static FieldInfo _parentBundleField =
            Type.GetType("Cassette.FileAsset, Cassette")
                .GetField("parentBundle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

        private IUrlGenerator _inner = null;

        public UrlGenerator(IUrlModifier urlModifier, string cassetteHandlerPrefix)
        {
            _cassetteHandlerPrefix = cassetteHandlerPrefix;
            _inner = Activator.CreateInstance(_internalUrlGenerator, urlModifier, cassetteHandlerPrefix) as IUrlGenerator;
        }

        public string CreateAbsolutePathUrl(string applicationRelativePath)
        {
            return _inner.CreateAbsolutePathUrl(applicationRelativePath);
        }

        public string CreateAssetUrl(IAsset asset)
        {
            var theme = "";
            var parentBundle = GetReferenceBundle(asset);

            if (parentBundle != null)
            {
                if (parentBundle.HtmlAttributes.ContainsAttribute("data-theme"))
                {
                    theme = parentBundle.HtmlAttributes["data-theme"];
                }
            }

            var hash = Convert.ToBase64String(asset.Hash).Replace('+', '-').Replace('/', '_');
            var path = asset.Path + "?" + hash;
            if (path.StartsWith("~"))
                path = path.Substring(1);

            return new Url(path).AppendQuery("theme", theme);
        }

        public string CreateBundleUrl(Bundle bundle)
        {
            var theme = string.Empty;
            if (bundle.HtmlAttributes.ContainsAttribute("data-theme"))
            {
                theme = bundle.HtmlAttributes["data-theme"];
            }
            return new Url( _inner.CreateBundleUrl(bundle)).AppendQuery("theme", theme);
        }

        public string CreateRawFileUrl(string filename, string hash)
        {
            return _inner.CreateRawFileUrl(filename, hash);
        }

        private Bundle GetReferenceBundle(IAsset asset)
        {
            if (!(asset is FileAsset))
                return null;

            var parentBundle = _parentBundleField.GetValue(asset);

            return parentBundle as Bundle;
        }


        public string CreateCachedFileUrl(string filename)
        {
            return _inner.CreateCachedFileUrl(filename);
        }

        public string CreateRawFileUrl(string filename)
        {
            return _inner.CreateRawFileUrl(filename);
        }
    }
}

