using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Collections;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.ViewModels
{
    /// <summary>
    /// Base model for rendering a bootstrap-style navigation in many ways.
    /// This is a contract so that the view can be shared in multiple locations/ways.
    /// </summary>
    public interface INavigationModel
    {
        bool WrapInWell { get; set; }
        bool Stacked { get; set; }
        bool AllowDropDown { get; set; }
        ItemFilter Filtler { get; set; }
        NavigationTypeEnum NavigationType { get; set; }
        ContentItem StartFrom { get; set; }
    }
}
