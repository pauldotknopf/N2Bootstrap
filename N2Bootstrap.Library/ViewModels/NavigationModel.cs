﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;

namespace N2Bootstrap.Library.ViewModels
{
    public class NavigationModel : INavigationModel
    {
        public bool WrapInWell { get; set; }
        public bool Stacked { get; set; }
        public bool AllowDropDown { get; set; }
        public bool IncludeRoot { get; set; }
        public N2.Collections.ItemFilter Filtler { get; set; }
        public NavigationTypeEnum NavigationType { get; set; }
        public ContentItem StartFrom { get; set; }
        public ContentItem CurrentItem { get; set; }
    }
}
