using System.Linq;
using N2;
using N2.Engine;
using N2.Web.Parts;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Adapters
{
    /// <summary>
    /// Implements "Recusive" zones functionality.
    /// </summary>
    [Adapts(typeof(ContentPage))]
    public class RecursiveZonesAdapter : PartsAdapter
    {
        public override System.Collections.Generic.IEnumerable<ContentItem> GetParts(ContentItem page, string zoneName, string @interface)
        {
            var items = base.GetParts(page, zoneName, @interface);

            var pageParent = page.VersionOf.HasValue
                ? (ContentItem)page.VersionOf.Parent
                : page.Parent;

            if (pageParent != null && zoneName.StartsWith("Recursive"))
                return items.Union(GetParts(pageParent, zoneName, @interface));

            return items;
        }
    }
}