using System;
using Cassette.IO;

namespace N2Bootstrap.Library.Cassette
{
    public class VirtualFile : IFile
    {
        private readonly string _path;

        public VirtualFile(string path)
        {
            _path = path;
        }

        public void Delete()
        {
            // can't do
        }

        public IDirectory Directory
        {
            get
            {
                return new CassetteVirtualDirectory(_path.Substring(0, _path.LastIndexOf('/') + 1));
            }
        }

        public bool Exists
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(ConvertPath(_path));
            }
        }

        public string FullPath
        {
            get
            {
                return ConvertPath(_path);
            }
        }

        public DateTime LastWriteTimeUtc
        {
            get { return DateTime.UtcNow; }
        }

        public System.IO.Stream Open(System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare fileShare)
        {
            return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(ConvertPath(_path)).Open();
        }

        public string ConvertPath(string path)
        {
            if (path.StartsWith("~/"))
                return path;
            return "~/" + path;
        }
    }
}
