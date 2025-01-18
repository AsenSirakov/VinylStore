using VinylStore.Models.DTO;

namespace VinylStore.Models.Requests
{
    public class AddVinylRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Song> Songs { get; set; } 
    }
}
