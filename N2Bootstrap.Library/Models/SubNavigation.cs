using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Navigation")]
    public class SubNavigation : PartModelBase, ViewModels.INavigationModel
    {
        [EditableCheckBox(Title = "", CheckBoxText = "Wrap in well", DefaultValue = false)]
        public virtual bool WrapInWell { get; set; }

        [EditableCheckBox(Title = "", CheckBoxText = "Stack nav vertically", DefaultValue = true)]
        public virtual bool Stacked { get; set; }

        [EditableCheckBox(Title = "", CheckBoxText = "Allow child dropdowns", DefaultValue = false)]
        public virtual bool AllowDropDown { get; set; }

        [EditableEnum(EnumType = typeof(NavigationTypeEnum), Title = "Navigation type", DefaultValue = NavigationTypeEnum.Tabs)]
        public virtual NavigationTypeEnum NavigationType { get; set; }

        [EditableLink(Title="Start from")]
        public virtual ContentItem StartFrom { get; set; }

        /// <summary>
        /// Not used, default navigation filter will be used
        /// </summary>
        public N2.Collections.ItemFilter Filtler { get; set; }
    }
}
