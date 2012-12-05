using System.Web.Mvc;
using N2.Web.Mvc;
using N2.Web.Mvc.Html;
using N2.Web;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library
{
    public static class HtmlHelpers
    {
        public static void BootstrapTree<TModel>(this HtmlHelper<TModel> helper)
        {
            //helper.Tree()
        }

        public static bool IsSiteZoneEditable<TModel>(this HtmlHelper<TModel> helper)
        {
            var state = helper.GetControlPanelState();
            var content = new DynamicContentHelper(helper);

            if (state.IsFlagSet(ControlPanelState.DragDrop))
            {
                if (content.Current.Item == content.Traverse.StartPage)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}