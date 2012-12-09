using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using N2;
using N2.Collections;
using N2.Definitions;
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

        /// <summary>
        /// Build a tree that is bootstrap friendly
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <param name="startFrom"></param>
        /// <param name="currentItem"></param>
        /// <param name="takeLevel"></param>
        /// <param name="filter"></param>
        /// <param name="appendCreatorNode"></param>
        /// <param name="disabledDropdowns"></param>
        /// <returns></returns>
        public static N2.Web.Tree BootstrapTree<TModel>(this HtmlHelper<TModel> helper, 
            N2.ContentItem startFrom = null,
            N2.ContentItem currentItem = null,
            int takeLevel = 2,
            ItemFilter filter = null,
            bool appendCreatorNode = false,
            bool onHoverDropdowns = false)
        {
            // prep
            if (startFrom == null)
            {
                startFrom = helper.StartPage();
            }
            if (currentItem == null)
            {
                currentItem = helper.CurrentPage();
            }

            var tree = helper.Tree(startFrom, currentItem, takeLevel, htmlAttributes: new { @class = "nav" }, appendCreatorNode: appendCreatorNode, filter: filter);
 
            // get the parents to mark 'active'
            var currentItemparents = new List<ContentItem>();
            if (currentItem != startFrom)
            {
                currentItemparents = Find.EnumerateParents(currentItem).Where(x => !(x is IRootPage) && x != startFrom).ToList();
            }

            tree.ClassProvider(
                (node) => node.Parent != null && node.Parent.Current != null ? "dropdown-menu hidden-tablet hidden-phone" : string.Empty,
                (node) =>
                {
                    // if children, indicate with a dropdown class
                    var @class = node.Children.Count > 0 ? "dropdown" : string.Empty;

                    // we only show active on first level
                    if (node.Parent == null || node.Parent.Current == null)
                    {
                        if (currentItemparents.Contains(node.Current) || node.Current == currentItem)
                        {
                            return (@class + " active").Trim();
                        }
                    }

                    return @class;
                })
                .LinkWriter((node, writer) =>
                {
                    var link = Link.To(node.Current);
                    if (node.Children.Count > 0)
                    {
                        link.Class(("dropdown-toggle " + (onHoverDropdowns ? "disabled" : "")).Trim());
                        link.Attribute("data-toggle", "dropdown");
                    }
                    link.WriteTo(writer);
                });

            return tree;
        }

        /// <summary>
        /// Add attibtues the root of a treeview
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static N2.Web.Tree HtmlAttibutes(this N2.Web.Tree tree, object htmlAttributes)
        {
            tree.Tag((node, builder) =>
                         {
                             if (builder.TagName.Equals("ul", StringComparison.InvariantCultureIgnoreCase)
                                 && (node.Parent == null || node.Parent.Current == null))
                             {
                                 builder.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
                             }
                         });
            return tree;
        }
    }
}