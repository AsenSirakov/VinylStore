using Mapster;
using VinylStore.Models.DTO;
using VinylStore.Models.Requests;

namespace VinylStore.MappsterConfig
{
    public class MappsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Vinyl, AddVinylRequest>
                .NewConfig()
                .TwoWays();
        }
    }
}
