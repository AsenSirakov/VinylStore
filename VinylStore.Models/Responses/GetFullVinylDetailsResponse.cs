using VinylStore.Models.Views;

namespace VinylStore.Models.Responses
{
    public class GetFullVinylDetailsResponse
    {
        public List<VinylView> Vinyls { get; set; } = [];
    }
}
