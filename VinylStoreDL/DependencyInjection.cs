using Microsoft.Extensions.DependencyInjection;
using VinylStoreDL.Interfaces;
using VinylStoreDL.Repositories;

namespace VinylStoreDL
{
    public static class DependencyInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddSingleton<IVinylRepository, VinylRepository>()
                .AddSingleton<ISongRepository, SongRepository>();
        }
    }
}
