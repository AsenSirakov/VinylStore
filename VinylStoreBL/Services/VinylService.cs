using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using VinylStoreDL.Interfaces;

namespace VinylStoreBL.Services
{
    public class VinylService : IVinylService
    {
        private readonly IVinylRepository _vinylRepository;
        private readonly ISongRepository _songRepository;

        public VinylService(IVinylRepository vinylRepository, ISongRepository songRepository)
        {
            _vinylRepository = vinylRepository;
            _songRepository = songRepository;
        }

        public List<Vinyl> GetAllVinyls()
        {
            return _vinylRepository.GetAllVinyls();
        }

        public void AddVinyl(Vinyl? vinyl)
        {
            if (vinyl is null) return;

            foreach (var vinylSong in vinyl.Songs)
            {
                var song = _songRepository.SongById(vinylSong.Id);

                if (song is null)
                {
                    throw new Exception(
                        $"Song with id {vinylSong.Id} does not exist");
                }
            }

            _vinylRepository.AddVinyl(vinyl);
        }

        public Vinyl? GetVinylById(string id)
        {
            return _vinylRepository.GetVinylById(id);
        }

        
        public bool DeleteVinylById(string id)
        {
            
            var vinyl = _vinylRepository.GetVinylById(id);
            if (vinyl == null)
            {
                return false; 
            }

            
            return _vinylRepository.DeleteVinylById(id);
        }
    }
}
