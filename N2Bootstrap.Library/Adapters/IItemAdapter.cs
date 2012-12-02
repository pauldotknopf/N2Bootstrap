using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.UI;
using N2.Definitions;

namespace N2Bootstrap.Library.Adapters
{
    public interface IItemAdapter
    {
        IDictionary<string, System.Web.UI.Control> AddDefinedEditors(
            ItemDefinition definition, 
            Control container, 
            IPrincipal user, 
            Type containerTypeFilter, 
            IEnumerable<string> editableNameFilter);
    }
}
