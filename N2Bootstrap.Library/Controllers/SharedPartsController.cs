using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Web;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Controllers
{
    /// <summary>
    /// This controller will handle parts deriving from AbstractItem which are not 
    /// defined by another controller [Controls(typeof(MyPart))]. The default 
    /// behavior is to render a template with this pattern:
    ///  * "~/Views/SharedParts/{ContentTypeName}.ascx"
    /// </summary>
    [Controls(typeof(PartModelBase))]
    public class SharedPartsController : TemplatesControllerBase<PartModelBase>
    {
    }
}
