using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Web;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Controllers
{
    /// <summary>
    /// This controller will handle pages deriving from AbstractPage which are not 
    /// defined by another controller [Controls(typeof(MyPage))]. The default 
    /// behavior is to render a template with this pattern:
    ///  * "~/Views/SharedPages/{ContentTypeName}.aspx"
    /// </summary>
    [Controls(typeof(PageModelBase))]
    public class SharedPagesController : TemplatesControllerBase<PageModelBase>
    {
    }
}