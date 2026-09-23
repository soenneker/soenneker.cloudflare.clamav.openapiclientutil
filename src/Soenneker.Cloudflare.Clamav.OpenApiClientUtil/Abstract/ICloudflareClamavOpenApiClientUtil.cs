using Soenneker.Cloudflare.Clamav.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ICloudflareClamavOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<CloudflareClamavOpenApiClient> Get(CancellationToken cancellationToken = default);
}
