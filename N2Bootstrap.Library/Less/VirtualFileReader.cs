using System.Collections.Generic;
using System.IO;
using dotless.Core.Input;

namespace N2Bootstrap.Library.Less
{
    public class VirtualFileReader : IFileReader
    {
        private readonly IPathResolver _pathResolver;
        private readonly HashSet<string> _importFilePaths;

        public VirtualFileReader(HashSet<string> importFilePaths)
        {
            _importFilePaths = importFilePaths;
        }

        public bool DoesFileExist(string fileName)
        {
            return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(fileName);
        }

        public string GetFileContents(string fileName)
        {
            _importFilePaths.Add(fileName);

            using (var stream = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(fileName).Open())
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        public byte[] GetBinaryFileContents(string fileName)
        {
            _importFilePaths.Add(fileName);

            using (var stream = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(fileName).Open())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
