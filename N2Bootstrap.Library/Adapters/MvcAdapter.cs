using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Engine;
using N2.Web.Mvc;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Adapters
{
    [Adapts(typeof(ModelBase))]
    public class ModelMvcAdapter : MvcAdapter
    {
        public override void RenderTemplate(System.Web.Mvc.HtmlHelper html, N2.ContentItem model)
        {
            var wrap = !model.IsPage && Defaults.IsContainerWrappable(model.ZoneName);

            if (wrap)
            {
                wrap = model.GetDetail("UseContainer", true);
            }

            if (wrap)
            {
                html.ViewContext.Writer.WriteLine("<div class=\"container\">");
            }
            base.RenderTemplate(html, model);
            if (wrap)
            {
                html.ViewContext.Writer.WriteLine("</div>");
            }
        }
    }
}
