using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using BundleTransformer.Core;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.HttpHandlers;
using BundleTransformer.Core.Translators;

namespace N2Bootstrap.Library.Less
{
    public class LessHandler : AssetHandlerBase
    {
        /// <summary>
		/// Asset content type
		/// </summary>
		public override string ContentType
		{
			get { return BundleTransformer.Core.Constants.ContentType.Css; }
		}


		/// <summary>
		/// Constructs a instance of base LESS asset handler
		/// </summary>
		/// <param name="cache">Server cache</param>
		/// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
		/// <param name="assetHandlerConfig">Configuration settings of the debugging HTTP-handler,
		/// that responsible for text output of processed asset</param>
        protected LessHandler(Cache cache,
			IVirtualFileSystemWrapper virtualFileSystemWrapper,
			AssetHandlerSettings assetHandlerConfig)
			: base(cache, virtualFileSystemWrapper, assetHandlerConfig)
		{ }

        /// <summary>
		/// Constructs a instance of LESS asset handler
		/// </summary>
        public LessHandler()
			: this(HttpContext.Current.Cache,
				BundleTransformerContext.Current.GetVirtualFileSystemWrapper(),
				BundleTransformerContext.Current.GetCoreConfiguration().AssetHandler)
		{ }


		/// <summary>
		/// Translates a code of asset written on LESS to CSS-code
		/// </summary>
		/// <param name="asset">Asset with code written on LESS</param>
		/// <param name="isDebugMode">Flag that web application is in debug mode</param>
		/// <returns>Asset with translated code</returns>
		protected override IAsset ProcessAsset(IAsset asset, bool isDebugMode)
		{
            var translater = new LessTranslater(Theme);
		    translater.IsDebugMode = isDebugMode;
			return translater.Translate(asset);
		}

        public override string GetCacheKey(string assetUrl)
        {
            return base.GetCacheKey(assetUrl) + "-theme-" + Theme;
        }

        private string Theme
        {
            get  { return HttpContext.Current.Request["theme"] ?? ""; }
        }
    }
}
