using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using N2.Web;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Utils;

namespace N2Bootstrap.Library.Less
{
    public class Importer : dotless.Core.Importers.Importer
    {
        private readonly string _theme;
        private Stack<string> _imports;

        public Importer(HashSet<string> importFilePaths, string file, string theme)
            :base(new VirtualFileReader(importFilePaths))
        {
            _theme = theme;
            _imports = new Stack<string>();
            _imports.Push(GetDirectory(file));
        }

        public override ImportAction Import(dotless.Core.Parser.Tree.Import import)
        {
            // get the current directory we are importing from
            var currentDirectory = _imports.Peek();

            // get the themed file
            import.Path = GetThemedImport(currentDirectory, import.Path);

            // now that we have a new path, get it queued up for future future imports that this new file may have
            _imports.Push(GetDirectory(import.Path));

            // perform the import (and maybe some nested imports)
            var result = base.Import(import);

            // we are done with this directory
            _imports.Pop();

            return result;
        }

        private string GetThemedImport(string currentDirectory, string fileName)
        {
            return GetThemedImport(currentDirectory, fileName, _theme);
        }

        private string GetThemedImport(string currentDirectory, string fileName, string theme)
        {
            string result = fileName.StartsWith("~")
                ? fileName.ToLower()
                : Path.Combine(currentDirectory, fileName).ToLower();

            return ThemedLessEngine.GetThemedFile(result, theme);
        }

        private string GetDirectory(string file)
        {
            return file.Replace("\\\\", "\\").Replace("\\", "/").Substring(0, file.LastIndexOf('/') + 1).Replace("~/", "/");
        }
    }
}
