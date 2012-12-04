using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Cassette;
using N2Bootstrap.Library.Less;
using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Parser;
using dotless.Core.Stylizers;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class ThemedLessHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(ThemedLessEngine.CompileLess(context.Request.Url.LocalPath, null, context.Request.QueryString["theme"]).Output);
            context.Response.ContentType = "text/css";
        }
    }
}
