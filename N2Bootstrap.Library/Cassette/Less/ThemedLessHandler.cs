using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Cassette;
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
            //string localPath = "~" + context.Request.Url.LocalPath;

            //string fileContents;
            //using (var sr = new StreamReader(System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(localPath).Open()))
            //{
            //    fileContents = sr.ReadToEnd();
            //}

            //var importedFilePaths = new HashSet<string>();
            //var engine = new LessEngine(new Parser(new ConsoleStylizer(), new Importer(new VirtualFileReader(localPath, importedFilePaths))));//context.SourceFilePath, importedFilePaths))));
            //var result = engine.TransformToCss(fileContents, localPath);

            context.Response.Write(ThemedLessEngine.CompileLess(context.Request.Url.LocalPath).Output);
            context.Response.ContentType = "text/css";
        }
    }
}
