using Microsoft.Extensions.DependencyInjection;
using VinylStoreBL.Interfaces;
using VinylStoreBL.Services;
using VinylStoreDL;

namespace VinylStoreBL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IVinylService, VinylService>();
            services.AddSingleton<IVinylBlService, VinylBlService>();
            services.AddSingleton<ISongService, SongService>();

            return services;
        }

        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            services.RegisterRepositories();

            return services;
        }
    }
}
