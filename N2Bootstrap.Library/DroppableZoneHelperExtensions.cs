using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using N2.Web.Mvc.Html;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library
{
    public static class DroppableZoneHelperExtensions
    {
        private static FieldInfo _stateField = typeof(DroppableZoneHelper).GetField("state",
                                                                                     BindingFlags.Public |
                                                                                     BindingFlags.NonPublic |
                                                                                     BindingFlags.Instance |
                                                                                     BindingFlags.Static);
        public static DroppableZoneHelper SetState(this DroppableZoneHelper helper, ControlPanelState state)
        {
            _stateField.SetValue(helper, state);
            return helper;
        }
    }
}
