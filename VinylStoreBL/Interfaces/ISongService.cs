using VinylStore.Models.DTO;

namespace VinylStoreBL.Interfaces
{
    public interface ISongService
    {
        void Add(Song song);

        // New method to retrieve a song by its ID
        Song? GetById(string id);

        // New method to retrieve all songs
        IEnumerable<Song> GetAll();
    }
}
