using System.IO;
using System.Web.Mvc;
using N2;
using N2.Engine;
using N2.Web.Mvc;
using N2.Web.Parts;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Adapters
{
    /// <summary>
    /// Adapts 
    /// </summary>
    [Adapts(typeof(PartModelBase))]
    public class PartBootstrapContainerAdapter : PartsAdapter
    {
        public override void RenderPart(HtmlHelper html, ContentItem part, TextWriter writer = null)
        {
            var wrap = Defaults.IsContainerWrappable(part.ZoneName) && part.GetDetail("UseContainer", true);

            if (wrap)
            {
                html.ViewContext.Writer.WriteLine("<div class=\"container\">");
            }
            base.RenderPart(html, part);
            if (wrap)
            {
                html.ViewContext.Writer.WriteLine("</div>");
            }
        }
    }
}
