using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2;
using N2.Web.Mvc;
using N2.Web.Mvc.Html;
using N2.Web;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Render a list of links of content items with a separator
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TContentItem"></typeparam>
        /// <param name="helper"></param>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="linkModifier"></param>
        /// <returns></returns>
        public static IHtmlString ContentItemLinkList<TModel, TContentItem>
            (this HtmlHelper<TModel> helper,
            IEnumerable<TContentItem> items,
            string separator = ", ",
            Action<ILinkBuilder> linkModifier = null)
            where TContentItem : ContentItem
        {
            var html = string.Join(separator, items.Select(x =>
            {
                var link = Link.To(x);
                if (linkModifier != null)
                    linkModifier(link);
                return link.ToString();
            }));
            return MvcHtmlString.Create(html);
        }
    }
}