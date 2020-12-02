using System;
using Auth.NET.Functions;
using Auth.NET.Models;
using Auth.NET.Requests;
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Auth.NET
{
    public static class InstallAuthService
    {
        public static IServiceCollection AddGoogleAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<GoogleOptions>(configuration.GetSection("Authentication:Google"));
            services.AddScoped<GoogleAuth>();
            services.AddBlazoredLocalStorage();

            services.AddHttpClient("google", client =>
                {
                    var config = new GoogleOptions();
                    configuration.GetSection("Authentication:Google").Bind(config);
                    client.BaseAddress = new Uri(config.ApiBaseAddress);
                })
                .AddTypedClient(RestService.For<IGoogleRequests>);
            
            return services;
        }
        
        public static IServiceCollection AddMicrosoftAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MicrosoftOptions>(configuration.GetSection("Authentication:Microsoft"));
            services.AddScoped<MicrosoftAuth>();
            services.AddBlazoredLocalStorage();

            services.AddHttpClient("microsoft", client =>
                {
                    var config = new MicrosoftOptions();
                    configuration.GetSection("Authentication:Microsoft").Bind(config);
                    client.BaseAddress = new Uri($"https://{config.BaseAddress}/");
                })
                .AddTypedClient(RestService.For<IMicrosoftRequests>);
            
            return services;
        }
    }
}