using Cassette;
using Cassette.Stylesheets;
using Cassette.TinyIoC;
using N2Bootstrap.Library.Cassette.Less;

namespace N2Bootstrap.Library.Cassette
{
    public class CassetteIoCConfiguation : IConfiguration<TinyIoCContainer>
    {
        public void Configure(TinyIoCContainer configurable)
        {
            configurable.Register<ILessCompiler, CassetteLessCompiler>().AsMultiInstance();
            configurable.Register<IUrlGenerator>((c, n) => new UrlGenerator(c.Resolve<IUrlModifier>(), "cassette.axd/"));
        }
    }
}
