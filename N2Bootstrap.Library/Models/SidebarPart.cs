using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Details;

namespace N2Bootstrap.Library.Models
{
    [WithEditableTitle(SortOrder = 100)]
    public abstract class SidebarPart : PartModelBase
    {
        [EditableCheckBox(Title = "Show title", CheckBoxText = "", DefaultValue = true, SortOrder = 101)]
        public virtual bool ShowTitle { get; set; }
    }
}
