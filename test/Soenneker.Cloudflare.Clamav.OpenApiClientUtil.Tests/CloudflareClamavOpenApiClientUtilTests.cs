using Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CloudflareClamavOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ICloudflareClamavOpenApiClientUtil _openapiclientutil;

    public CloudflareClamavOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ICloudflareClamavOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
