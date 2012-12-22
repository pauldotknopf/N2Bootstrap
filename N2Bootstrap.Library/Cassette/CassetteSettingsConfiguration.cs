using Cassette;

namespace N2Bootstrap.Library.Cassette
{
    public class CassetteSettingsConfiguration : IConfiguration<CassetteSettings>
    {
        public void Configure(CassetteSettings configurable)
        {
            configurable.SourceDirectory = new CassetteVirtualDirectory();
            configurable.IsDebuggingEnabled = false;
        }
    }
}
