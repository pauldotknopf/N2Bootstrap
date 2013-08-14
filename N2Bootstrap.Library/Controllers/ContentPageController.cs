using System.Web.Mvc;
using N2.Web;
using N2.Web.Mvc;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Controllers
{
    /// <summary>
    /// This controller will handle pages deriving from AbstractPage which are not 
    /// defined by another controller [Controls(typeof(MyPage))]. The default 
    /// behavior is to render a template with this pattern:
    ///  * "~/Views/ContentPage/{ContentTypeName}.cshtml"
    /// </summary>
    [Controls(typeof(ContentPage))]
    public class ContentPageController : ContentController<ContentPage>
    {
        public override ActionResult Index()
        {
            return View(CurrentItem.TemplateKey, CurrentItem);
        }
    }
}