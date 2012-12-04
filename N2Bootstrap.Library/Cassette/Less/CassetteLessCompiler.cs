using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Cassette;
using N2.Web.Mvc.Html;
using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Parser;
using dotless.Core.Stylizers;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class CassetteLessCompiler : global::Cassette.Stylesheets.ILessCompiler
    {
        public CompileResult Compile(string source, CompileContext context)
        {
            return ThemedLessEngine.CompileLess(context.SourceFilePath, source);
            //var importedFilePaths = new HashSet<string>();
            //var engine = new LessEngine(new Parser(new ConsoleStylizer(), new Importer(new VirtualFileReader(context.SourceFilePath, importedFilePaths))));
            //var result = engine.TransformToCss(source, context.SourceFilePath);
            //return new CompileResult(result, importedFilePaths);
        }
    }
}
