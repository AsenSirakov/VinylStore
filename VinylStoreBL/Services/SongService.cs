using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using VinylStoreDL.Interfaces;

namespace VinylStoreBL.Services
{
    internal class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public void Add(Song song)
        {
            _songRepository.AddSong(song);
        }

        
        public Song? GetById(string id)
        {
            return _songRepository.SongById(id);
        }

        
        public IEnumerable<Song> GetAll()
        {
            return _songRepository.GetAllSongs();
        }
    }
}
