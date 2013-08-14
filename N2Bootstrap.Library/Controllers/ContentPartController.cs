using System.Web.Mvc;
using N2;
using N2.Web;
using N2.Web.Mvc;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Controllers
{
    /// <summary>
    /// Returns the partial view for the <see cref="ContentPart"/> based on the selected template key.
    /// </summary>
    /// <remarks>
    /// Multiple templates for <see cref="ContentPart"/> can be added to the view folder, and the corresponding
    /// template will be matched for the instance based on the <see cref="ContentItem.TemplateKey"/> property.
    /// </remarks>
    [Controls(typeof(ContentPart))]
    public class ContentPartController : ContentController<ContentPart>
    {
        public override ActionResult Index()
        {
            return PartialView(CurrentItem.TemplateKey, CurrentItem);
        }
    }
}