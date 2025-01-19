using VinylStore.Models.DTO;

namespace VinylStoreDL.Interfaces
{
    public interface ISongRepository
    {
        void AddSong(Song song);

        IEnumerable<Song> GetSongsByIds(IEnumerable<string> songIds);

        Song? SongById(string id);

        
        IEnumerable<Song> GetAllSongs();

        bool DeleteSongById(string id);

        bool UpdateSong(Song updatedSong);

    }
}
