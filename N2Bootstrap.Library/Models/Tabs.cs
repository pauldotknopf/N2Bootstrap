using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;
using N2.Integrity;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Tabs", IconUrl = "{IconsUrl}/tab.png")]
    [RestrictChildren(typeof(Tab))]
    public class Tabs : PartModelBase
    {
        [EditableChildren("Tabs", "Tabs", 1)]
        public List<Tabs.Tab> TabsList { get; set; }

        [EditableEnum(Title="Tabs position", EnumType=typeof(TabPositionEnum), DefaultValue=TabPositionEnum.Top)]
        public virtual TabPositionEnum TabPosition { get; set; }

        [PartDefinition("Tab", IconUrl = "{IconsUrl}/tab_add.png")]
        [RestrictParents(typeof(Tabs))]
        [AllowedZones("Tabs")]
        [WithEditableTitle]
        public class Tab : ContentItem
        {
        }

        public enum TabPositionEnum
        {
            Top,
            Right,
            Bottom,
            Left
        }
    }
}
