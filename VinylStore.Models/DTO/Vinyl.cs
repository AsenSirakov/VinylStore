namespace VinylStore.Models.DTO
{
    public class Vinyl
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
