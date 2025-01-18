using VinylStore.Models.DTO;

namespace VinylStore.Models.Responses
{
    public class GetDetailedVinylResponse
    {
        public Vinyl Vinyl { get; set; }

        public List<Song> Songs { get; set; }
    }
}
