using System;
using System.Collections.Generic;
using Cassette.IO;

namespace N2Bootstrap.Library.Cassette
{
    public class CassetteVirtualDirectory : IDirectory
    {
        private readonly string _rootPath;

        public CassetteVirtualDirectory(string rootPath = "~/")
        {
            _rootPath = ConvertPath(rootPath);
        }

        public System.IO.FileAttributes Attributes
        {
            get { return System.IO.FileAttributes.ReadOnly; }
        }

        public void Create()
        {
            throw new InvalidOperationException("You cannot create a directory within virtual hosting.");
        }

        public void Delete()
        {
            throw new InvalidOperationException("You cannot delete a directory within virtual hosting.");
        }

        public bool DirectoryExists(string path)
        {
            return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.DirectoryExists(ConvertPath(path));
        }

        public bool Exists
        {
            get { return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.DirectoryExists(ConvertPath(_rootPath)); }
        }

        public string FullPath
        {
            get { return _rootPath; }
        }

        public System.Collections.Generic.IEnumerable<IDirectory> GetDirectories()
        {
            var directories = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetDirectory(ConvertPath(_rootPath)).Directories;
            return new List<IDirectory>();
        }

        public IDirectory GetDirectory(string path)
        {
            return new CassetteVirtualDirectory(path);
        }

        public IFile GetFile(string filename)
        {
            var fileNameConverted = ConvertPath(filename);
            return new VirtualFile(fileNameConverted);
        }

        public System.Collections.Generic.IEnumerable<IFile> GetFiles(string searchPattern, System.IO.SearchOption searchOption)
        {
            var files = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetDirectory(ConvertPath(_rootPath)).Files;
            return new List<IFile>();
        }

        public System.IDisposable WatchForChanges(System.Action<string> pathCreated, System.Action<string> pathChanged, System.Action<string> pathDeleted, System.Action<string, string> pathRenamed)
        {
            return null;
        }

        public string ConvertPath(string path)
        {
            if (path.StartsWith("~/"))
                return path;
            return "~/" + path;
        }
    }
}
