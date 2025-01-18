using VinylStore.Models.DTO;

namespace VinylStore.Models.Views
{
    public class VinylView
    {
        public string VinylId { get; set; } = string.Empty;

        public string VinylName { get; set; } = string.Empty;

        public IEnumerable<Song> Songs { get; set; } = new List<Song>();
    }
}
