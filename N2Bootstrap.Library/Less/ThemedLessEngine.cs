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
using N2Bootstrap.Library.Models;
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

        public static CompileResult CompileLess(string file, string contents, string theme)
        {
            return CompileLess(file, contents, theme, GetThemeVariables(BootstrapThemeConfiguration.GetOrCreateThemeConfiguration(theme)));
        }

        public static CompileResult CompileLess(string file, string contents, string theme, Dictionary<string, string> themeLessVariables)
        {
            if (string.IsNullOrEmpty(theme))
                theme = "Default";

            var importedFilePaths = new HashSet<string>();
            var engine = new LessEngine(new Parser(new ConsoleStylizer(), new Importer(importedFilePaths, file, theme)))
            {
                Plugins = new List<IPluginConfigurator>
                {
                    new VariableOverridePluginConfigurator(themeLessVariables)
                }
            };
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

        public static Dictionary<string, string> GetThemeVariables(BootstrapThemeConfiguration configuration)
        {
            var variables = new Dictionary<string, string>();
            var definition = N2.Context.Definitions.GetDefinition(configuration);
            foreach (var lessVariableEditable in definition.Editables.Where(x => x is Details.EditableLessVariableAttribute).Cast<Details.EditableLessVariableAttribute>())
            {
                if (variables.ContainsKey(lessVariableEditable.LessVariableName))
                    throw new Exception("You cannot have multipe less variable declarations (" + lessVariableEditable.LessVariableName + ")");
                variables[lessVariableEditable.LessVariableName] = (string)configuration[lessVariableEditable.Name];
            }
            return variables;
        }
    }

    

    
}
