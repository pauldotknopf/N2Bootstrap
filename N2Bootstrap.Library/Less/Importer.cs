using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Utils;

namespace N2Bootstrap.Library.Less
{
    public class Importer : dotless.Core.Importers.Importer
    {
        private readonly VirtualFileReader _fileReader;

        public Importer(VirtualFileReader fileReader)
            :base(fileReader)
        {
            _fileReader = fileReader;
        }

        public override ImportAction Import(dotless.Core.Parser.Tree.Import import)
        {
            import.Path = _fileReader.GetFilePath(import.Path);
            return base.Import(import);
        }

        //private static readonly Regex _embeddedResourceRegex = new Regex("^dll://(?<Assembly>.+?)#(?<Resource>.+)$");
        //protected readonly List<string> _paths;
        //protected readonly List<string> _rawImports;

        //public Importer()
        //    : this(new dotless.Core.Input.FileReader())
        //{
        //}

        //public Importer(IFileReader fileReader)
        //    : this(fileReader, false, false, false)
        //{
        //}

        //public Importer(IFileReader fileReader, bool disableUrlReWriting, bool inlineCssFiles, bool importAllFilesAsLess)
        //{
        //    this._paths = new List<string>();
        //    this._rawImports = new List<string>();
        //    this.FileReader = fileReader;
        //    this.IsUrlRewritingDisabled = disableUrlReWriting;
        //    this.InlineCssFiles = inlineCssFiles;
        //    this.ImportAllFilesAsLess = importAllFilesAsLess;
        //    this.Imports = new List<string>();
        //}

        //public string AlterUrl(string url, List<string> pathList)
        //{
        //    if (!(((!pathList.Any<string>() || this.IsUrlRewritingDisabled) || IsProtocolUrl(url)) || IsNonRelativeUrl(url)))
        //    {
        //        return this.GetAdjustedFilePath(url, pathList);
        //    }
        //    return url;
        //}

        //protected bool CheckIgnoreImport(dotless.Core.Parser.Tree.Import import)
        //{
        //    return this.CheckIgnoreImport(import, import.Path);
        //}

        //protected bool CheckIgnoreImport(dotless.Core.Parser.Tree.Import import, string path)
        //{
        //    if (this._rawImports.Contains<string>(path, StringComparer.InvariantCultureIgnoreCase))
        //    {
        //        return import.IsOnce;
        //    }
        //    this._rawImports.Add(path);
        //    return false;
        //}

        //protected string GetAdjustedFilePath(string path, List<string> pathList)
        //{
        //    return pathList.Concat<string>(new string[] { path }).AggregatePaths(this.CurrentDirectory);
        //}

        //public List<string> GetCurrentPathsClone()
        //{
        //    return new List<string>(this._paths);
        //}

        //public virtual ImportAction Import(dotless.Core.Parser.Tree.Import import)
        //{
        //    if (IsProtocolUrl(import.Path) && !IsEmbeddedResource(import.Path))
        //    {
        //        if (import.Path.EndsWith(".less"))
        //        {
        //            throw new FileNotFoundException(".less cannot import non local less files.", import.Path);
        //        }
        //        if (this.CheckIgnoreImport(import))
        //        {
        //            return ImportAction.ImportNothing;
        //        }
        //        return ImportAction.LeaveImport;
        //    }
        //    string path = import.Path;
        //    if (!IsNonRelativeUrl(path))
        //    {
        //        path = this.GetAdjustedFilePath(import.Path, this._paths);
        //    }
        //    if (this.CheckIgnoreImport(import, path))
        //    {
        //        return ImportAction.ImportNothing;
        //    }
        //    if ((!this.ImportAllFilesAsLess && import.Path.EndsWith(".css")) && !import.Path.EndsWith(".less.css"))
        //    {
        //        if (this.InlineCssFiles)
        //        {
        //            if (IsEmbeddedResource(import.Path) && this.ImportEmbeddedCssContents(path, import))
        //            {
        //                return ImportAction.ImportCss;
        //            }
        //            if (this.ImportCssFileContents(path, import))
        //            {
        //                return ImportAction.ImportCss;
        //            }
        //        }
        //        return ImportAction.LeaveImport;
        //    }
        //    if (this.Parser == null)
        //    {
        //        throw new InvalidOperationException("Parser cannot be null.");
        //    }
        //    if (!this.ImportLessFile(path, import))
        //    {
        //        if (import.Path.EndsWith(".less", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            throw new FileNotFoundException("You are importing a file ending in .less that cannot be found.", import.Path);
        //        }
        //        return ImportAction.LeaveImport;
        //    }
        //    return ImportAction.ImportLess;
        //}

        //protected bool ImportCssFileContents(string file, dotless.Core.Parser.Tree.Import import)
        //{
        //    if (!this.FileReader.DoesFileExist(file))
        //    {
        //        return false;
        //    }
        //    import.InnerContent = this.FileReader.GetFileContents(file);
        //    this.Imports.Add(file);
        //    return true;
        //}

        //private bool ImportEmbeddedCssContents(string file, dotless.Core.Parser.Tree.Import import)
        //{
        //    string str = ResourceLoader.GetResource(file, this.FileReader, out file);
        //    if (str == null)
        //    {
        //        return false;
        //    }
        //    import.InnerContent = str;
        //    return true;
        //}

        //protected bool ImportLessFile(string lessPath, dotless.Core.Parser.Tree.Import import)
        //{
        //    string fileDependency = null;
        //    string fileContents;
        //    if (IsEmbeddedResource(lessPath))
        //    {
        //        fileContents = ResourceLoader.GetResource(lessPath, this.FileReader, out fileDependency);
        //        if (fileContents == null)
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        bool flag3 = this.FileReader.DoesFileExist(lessPath);
        //        if (!(flag3 || lessPath.EndsWith(".less")))
        //        {
        //            lessPath = lessPath + ".less";
        //            flag3 = this.FileReader.DoesFileExist(lessPath);
        //        }
        //        if (!flag3)
        //        {
        //            return false;
        //        }
        //        fileContents = this.FileReader.GetFileContents(lessPath);
        //        fileDependency = lessPath;
        //    }
        //    this._paths.Add(Path.GetDirectoryName(import.Path));
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(fileDependency))
        //        {
        //            this.Imports.Add(fileDependency);
        //        }
        //        import.InnerRoot = this.Parser().Parse(fileContents, lessPath);
        //    }
        //    catch
        //    {
        //        this.Imports.Remove(fileDependency);
        //        throw;
        //    }
        //    finally
        //    {
        //        this._paths.RemoveAt(this._paths.Count - 1);
        //    }
        //    return true;
        //}

        //private static bool IsEmbeddedResource(string path)
        //{
        //    return _embeddedResourceRegex.IsMatch(path);
        //}

        //private static bool IsNonRelativeUrl(string url)
        //{
        //    return ((url.StartsWith("/") || url.StartsWith("~/")) || Regex.IsMatch(url, "^[a-zA-Z]:"));
        //}

        //private static bool IsProtocolUrl(string url)
        //{
        //    return Regex.IsMatch(url, "^([a-zA-Z]{2,}:)");
        //}

        //protected virtual string CurrentDirectory
        //{
        //    get
        //    {
        //        return Environment.CurrentDirectory;
        //    }
        //}

        //public static Regex EmbeddedResourceRegex
        //{
        //    get
        //    {
        //        return _embeddedResourceRegex;
        //    }
        //}

        //public IFileReader FileReader { get; set; }

        //public bool ImportAllFilesAsLess { get; set; }

        //public List<string> Imports { get; set; }

        //public bool InlineCssFiles { get; set; }

        //public bool IsUrlRewritingDisabled { get; set; }

        //public Func<dotless.Core.Parser.Parser> Parser { get; set; }
    }
}
