﻿using System.Collections.Generic;
using System.IO;
using dotless.Core.Input;

namespace N2Bootstrap.Library.Less
{
    public class VirtualFileReader : IFileReader
    {
        private readonly IPathResolver _pathResolver;
        private readonly HashSet<string> _importFilePaths;
        private readonly string _theme;

        private readonly string _startingDirectory;

        public VirtualFileReader(string original, HashSet<string> importFilePaths, string theme)
        {
            _importFilePaths = importFilePaths;
            _theme = theme;
            _startingDirectory = original.Substring(0, original.LastIndexOf('/') + 1);
        }

        public bool DoesFileExist(string fileName)
        {
            return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(GetFilePath(fileName));
        }

        public string GetFileContents(string fileName)
        {
            _importFilePaths.Add(fileName);

            using (var stream = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(GetFilePath(fileName)).Open())
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        public byte[] GetBinaryFileContents(string fileName)
        {
            _importFilePaths.Add(fileName);

            using (var stream = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(GetFilePath(fileName)).Open())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public string GetFilePath(string fileName)
        {
            string result = fileName.StartsWith("~") || fileName.StartsWith("/")
                ? fileName.ToLower() 
                : Path.Combine(_startingDirectory, fileName).ToLower();

            return ThemedLessEngine.GetThemedFile(result, _theme);
        }
    }
}