namespace VinylStore.Models.DTO
{
    public class Song
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        public Singer Singer { get; set; } = null!;
    }
}
