using Bonzai.Blazor.Bootstrap.Js;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bonzai.Blazor.Bootstrap
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection UseBonzaiBootstrap(this IServiceCollection services,
            string jsLibraryLocation = "/_content/Bonzai.Blazor.Bootstrap/Bonzai.Blazor.js")
        {
            BootstrapJsService.JsModuleLocation = jsLibraryLocation;

            services.AddScoped<IBootstrapJsService, BootstrapJsService>();

            return services;
        }
    }
}