using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Cassette;
using N2.Web.Mvc.Html;
using N2Bootstrap.Library.Less;
using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Parser;
using dotless.Core.Stylizers;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class CassetteLessCompiler : ICompiler
    {
        private readonly string _theme;

        public CassetteLessCompiler(string theme)
        {
            _theme = theme;
        }

        public CompileResult Compile(string source, CompileContext context)
        {
            return ThemedLessEngine.CompileLess(context.SourceFilePath, source, _theme);
        }
    }
}
