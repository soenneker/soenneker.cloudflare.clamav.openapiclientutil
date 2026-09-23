using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.Clamav.HttpClients.Registrars;
using Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Abstract;

namespace Soenneker.Cloudflare.Clamav.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class CloudflareClamavOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="CloudflareClamavOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareClamavOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloudflareClamavOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ICloudflareClamavOpenApiClientUtil, CloudflareClamavOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="CloudflareClamavOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareClamavOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareClamavOpenApiHttpClientAsSingleton()
                .TryAddScoped<ICloudflareClamavOpenApiClientUtil, CloudflareClamavOpenApiClientUtil>();

        return services;
    }
}
