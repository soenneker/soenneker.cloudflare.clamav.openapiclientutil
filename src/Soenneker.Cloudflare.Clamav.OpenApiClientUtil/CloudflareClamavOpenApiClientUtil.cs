using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Cloudflare.Clamav.HttpClients.Abstract;
using Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Abstract;
using Soenneker.Cloudflare.Clamav.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Cloudflare.Clamav.OpenApiClientUtil;

public sealed class CloudflareClamavOpenApiClientUtil : ICloudflareClamavOpenApiClientUtil
{
    private readonly AsyncSingleton<CloudflareClamavOpenApiClient> _client;

    public CloudflareClamavOpenApiClientUtil(ICloudflareClamavOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<CloudflareClamavOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Scanner:ApiKey");
            string authHeaderName = configuration["Scanner:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Scanner:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient)
            {
                BaseUrl = httpClient.BaseAddress!.AbsoluteUri
            };

            return new CloudflareClamavOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<CloudflareClamavOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
