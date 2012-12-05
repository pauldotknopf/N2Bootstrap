using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Engine;
using N2.Web;
using N2.Web.Parts;
using N2.Web.UI.WebControls;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Adapters
{
    [Adapts(typeof(ModelBase))]
    public class ModelPartsAdapter : PartsAdapter
    {
        public override IEnumerable<ContentItem> GetParts(ContentItem belowParentItem, string inZoneNamed, string filteredForInterface, N2.Web.UI.WebControls.ControlPanelState state)
        {
            var items = base.GetParts(belowParentItem, inZoneNamed, filteredForInterface, state);
            ContentItem grandParentItem = belowParentItem;
            //if (!state.IsFlagSet(ControlPanelState.DragDrop) && inZoneNamed.EndsWith("Recursive") && grandParentItem is ContentPage)
            if (inZoneNamed.EndsWith("Recursive") && grandParentItem is ContentPage)
            {
                if (!belowParentItem.VersionOf.HasValue)
                    items = items.Union(GetParts(belowParentItem.Parent, inZoneNamed, filteredForInterface));
                else
                    items = items.Union(GetParts(belowParentItem.VersionOf.Parent, inZoneNamed, filteredForInterface));
            }
            return items;
        }
    }
}
