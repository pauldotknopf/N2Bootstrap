using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Cassette;
using N2.Web.Mvc.Html;
using N2Bootstrap.Library.Cassette.Less;
using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Parser;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Tree;
using dotless.Core.Plugins;
using dotless.Core.Stylizers;
using Url = N2.Web.Url;

namespace N2Bootstrap.Library.Less
{
    public class ThemedLessEngine
    {
        public static bool IsThemeable(string file)
        {
            var themeDirectory = Url.ResolveTokens("{ThemesUrl}").ToLower();

            if (!file.StartsWith("~"))
            {
                file = "~" + file;
            }

            var isThemeable = file.StartsWith("~" + themeDirectory, StringComparison.InvariantCultureIgnoreCase);
            return isThemeable;
        }

        public static string GetThemedFile(string file, string theme)
        {
            // n2's zip provider doesn't support ../../, so lets try to fix it manually
            var regex = new Regex(@"([^/]*/\.\./)");
            while (regex.IsMatch(file))
            {
                file = regex.Replace(file, "");
            }


            file = file.Replace("\\\\", "\\").Replace("\\", "/");
            
            if (file.StartsWith("/"))
                file = "~" + file;

            if (!IsThemeable(file))
                return file;

            HttpContext.Current.Items["theme"] = theme;
            var themeDirectory = Url.ResolveTokens("{ThemesUrl}").ToLower();
            var themedLocation = file.Replace("~" + themeDirectory, "");
            themedLocation = themedLocation.Substring(themedLocation.IndexOf("/", StringComparison.Ordinal));
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var result = helper.ThemedContent(themedLocation);
            if (System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(result))
            {
                return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(result).VirtualPath;
            }
            return result;
        }

        public static CompileResult CompileLess(string file, string contents = null, string theme = null)
        {
            if (string.IsNullOrEmpty(theme))
                theme = "Default";

            var importedFilePaths = new HashSet<string>();
            var engine = new LessEngine(new Parser(new ConsoleStylizer(), new Importer(importedFilePaths, file, theme)));
            var plugins = new List<IPluginConfigurator>();
            plugins.Add(new PuginConfigurator());
            engine.Plugins = plugins;
            if (string.IsNullOrEmpty(contents))
            {
                using (var sr = new StreamReader(System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(file).Open()))
                {
                    contents = sr.ReadToEnd();
                }
            }
            var result = engine.TransformToCss(contents, file);
            return new CompileResult(result, importedFilePaths);
        }
    }

    public class Plugin : VisitorPlugin
    {
        public override VisitorPluginType AppliesTo
        {
            get { return VisitorPluginType.BeforeEvaluation; }
        }

        public override dotless.Core.Parser.Infrastructure.Nodes.Node Execute(dotless.Core.Parser.Infrastructure.Nodes.Node node, out bool visitDeeper)
        {
            visitDeeper = true;

            try
            {
                
            }
            catch (Exception ex)
            {
                return node;
            }

            if (node is Rule)
            {
                var rule = node as Rule;
                if (rule.Variable && rule.Name == "@bodyBackground")
                {
                    var parse = new Parser();
                    var ruleset = parse.Parse("@bodyBackground: @pink;", "dynamic.less");
                    return ruleset.Rules[0] as Rule;
                }
            }

            return node;
        }

        public override void OnPreVisiting(Env env)
        {
            base.OnPreVisiting(env);
        }

        public override void OnPostVisiting(Env env)
        {
            base.OnPostVisiting(env);
        }
    }

    public class PuginConfigurator : dotless.Core.Plugins.GenericPluginConfigurator<Plugin>
    {
        
    }
}
