using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2;
using N2.Definitions;
using N2.Web.Mvc;
using N2.Web.Mvc.Html;
using N2.Web.UI.WebControls;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library
{
	public static class Defaults
    {
        public static List<string> ContainerWrappableZones = new List<string> { "BeforeMain", "BeforeMainRecursive", "BeforeMainSite", "AfterMain", "AfterMainRecursive", "AfterMainSite" };

        public enum Columns
        {
            OneColumn,
            TwoColumn,
            ThreeColumn
        }

		public static class Containers
		{
			public const string Metadata = "Metadata";
			public const string Content = "Content";
			public const string Site = "Site";
			public const string Advanced = "Advanced";
		    public const string Layout = "Layout";
		}
		
		public static string ImageSize(string preferredSize, string fallbackToZoneNamed)
		{
			if (string.IsNullOrEmpty(preferredSize))
				return ImageSize(fallbackToZoneNamed);
			return preferredSize;
		}

		public static string ImageSize(string zoneName)
		{
			switch (zoneName)
			{
				case "SliderArea":
				case "PreContent":
				case "PostContent":
					return "wide";
				default:
					return "half";
			}
		}

        public static bool IsContainerWrappable(string zoneName)
        {
            if (string.IsNullOrEmpty(zoneName))
                return false;

            if (ContainerWrappableZones.Contains(zoneName))
                return true;

            return false;
        }

        /// <summary>
        /// Picks the translation best matching the browser-language or the first translation in the list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public static ContentItem SelectLanguage(this HttpRequestBase request, ContentItem currentPage)
        {
            var start = Find.ClosestOf<IStartPage>(currentPage) ?? N2.Find.StartPage;
            if (start == null) return null;

            if (start is LanguageIntersection)
            {
                var translations = GetTranslations(currentPage).ToList();

                if (request.UserLanguages == null)
                    return translations.FirstOrDefault();

                var selectedlanguage = request.UserLanguages.Select(ul => translations.FirstOrDefault(t => t.LanguageCode == ul)).FirstOrDefault(t => t != null);
                return selectedlanguage ?? translations.FirstOrDefault();
            }

            return start;
        }

        private static IEnumerable<StartPage> GetTranslations(ContentItem currentPage)
        {
            return currentPage.GetChildren().OfType<StartPage>();
        }
	}
}