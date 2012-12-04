using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cassette;
using Cassette.IO;

namespace N2Bootstrap.Library.Cassette.Less
{
    public class LessCompileAsset : IAssetTransformer
    {
        private readonly ICompiler compiler;
        private readonly IDirectory rootDirectory;

        public LessCompileAsset(ICompiler compiler, IDirectory rootDirectory)
        {
            this.compiler = compiler;
            this.rootDirectory = rootDirectory;
        }
        
        private void AddRawFileReferenceForEachImportedFile(IAsset asset, CompileResult compileResult)
        {
            foreach (string str in compileResult.ImportedFilePaths)
            {
                asset.AddRawFileReference(str);
            }
        }
        
        private CompileResult Compile(IAsset asset, StreamReader input)
        {
            CompileContext context = this.CreateCompileContext(asset);
            return this.compiler.Compile(input.ReadToEnd(), context);
        }
        
        private CompileContext CreateCompileContext(IAsset asset)
        {
            return new CompileContext { SourceFilePath = asset.Path, RootDirectory = this.rootDirectory };
        }
        
        public Func<Stream> Transform(Func<Stream> openSourceStream, IAsset asset)
        {
            return () => 
            {
                using (var reader = new StreamReader(openSourceStream()))
                {
                    var compileResult = this.Compile(asset, reader);
                    AddRawFileReferenceForEachImportedFile(asset, compileResult);
                    return AsStream(compileResult.Output);
                }
            };
        }

        private Stream AsStream(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0L;
            return stream;
        }
    }
}
