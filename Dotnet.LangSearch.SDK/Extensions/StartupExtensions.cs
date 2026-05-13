using Dotnet.LangSearch.SDK.Client;
using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Dotnet.LangSearch.SDK.Extensions
{
    public static class StartupExtensions
    {

        public static IServiceCollection AddLangSearchClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ILangSearchClient, LangSearchClient>(client =>
            {
                var cfg = configuration.GetSection(nameof(LangSearchSettings)).Get<LangSearchSettings>();
                client.BaseAddress = new Uri(cfg.Domain);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cfg.ApiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }

        public static IServiceCollection AddLangSearchService(this IServiceCollection services, IConfiguration configuration, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            => lifetime switch {
                ServiceLifetime.Scoped => services.AddScoped<ILangSearchService, LangSearchService>(),
                ServiceLifetime.Transient => services.AddTransient<ILangSearchService, LangSearchService>(),
                ServiceLifetime.Singleton => services.AddSingleton<ILangSearchService, LangSearchService>(),
                _ => services
            };

        public static IServiceCollection AddLangSearchConfiguration(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<LangSearchSettings>(configuration.GetSection(nameof(LangSearchSettings)));

        public static IServiceCollection ConfigureLangSearch(this IServiceCollection services, IConfiguration configuration)
            => services.AddLangSearchConfiguration(configuration)
                       .AddLangSearchClient(configuration)
                       .AddLangSearchService(configuration);
             
    }
}
