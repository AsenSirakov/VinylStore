using VinylStore.Models.DTO;

namespace VinylStoreBL.Interfaces
{
    public interface ISongService
    {
        void Add(Song song);

        
        Song? GetById(string id);

        
        IEnumerable<Song> GetAll();
    }
}
