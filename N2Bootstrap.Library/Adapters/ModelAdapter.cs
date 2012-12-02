using System;
using System.Collections.Generic;
using System.Linq;
using N2.Definitions;
using N2.Details;
using N2.Edit;
using N2.Engine;
using N2.Web.UI;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Adapters
{
    [Adapts(typeof(ModelBase))]
    public class ModelAdapter : EditableAdapter
    {
        public override IDictionary<string, System.Web.UI.Control> AddDefinedEditors(ItemDefinition definition, N2.ContentItem item, System.Web.UI.Control container, System.Security.Principal.IPrincipal user, Type containerTypeFilter, IEnumerable<string> editableNameFilter)
        {
            ItemDefinition cloned = null;

            // add a "wrap in container" checkbox to all parts that are within zones that are not wrapped in a container (BeforeMain, AfterMain).
            if (!definition.IsPage)
            {
                if (!(Defaults.IsContainerWrappable(item.ZoneName) ||
                      Defaults.IsContainerWrappable(System.Web.HttpContext.Current.Request["zoneName"])))
                {
                    cloned = definition.Clone();
                    var isWrappable = cloned.Editables.FirstOrDefault(x => x.Name == "UseContainer");
                    if (isWrappable != null)
                    {
                        cloned.Editables.Remove(isWrappable);
                    }
                }
            }

            if (item is IItemAdapter)
            {
                var result = (item as IItemAdapter).AddDefinedEditors(cloned ?? definition, container, user, containerTypeFilter, editableNameFilter);
                if (result != null)
                    return result;
            }
            return base.AddDefinedEditors(cloned ?? definition, item, container, user, containerTypeFilter, editableNameFilter);
        }
    }
}