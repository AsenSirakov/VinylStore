using VinylStore.Models.Views;
using VinylStoreBL.Interfaces;
using VinylStoreDL.Interfaces;

namespace VinylStoreBL.Services
{
    internal class VinylBlService : IVinylBlService
    {
        private readonly IVinylService _vinylService;
        private readonly ISongRepository _songRepository;

        public VinylBlService(
            IVinylService vinylService,
            ISongRepository songRepository)
        {
            _vinylService = vinylService;
            _songRepository = songRepository;
        }

        public List<VinylView> GetDetailedVinyls()
        {
            var result = new List<VinylView>();

            var vinyls = _vinylService.GetAllVinyls();

            foreach (var vinyl in vinyls)
            {
                List<string> songIds = new List<string>();
                foreach (var song in vinyl.Songs)
                {
                    songIds.Add(song.Id);
                }
                var vinylView = new VinylView
                {
                    VinylId = vinyl.Id,
                    VinylName = vinyl.Name,
                    Songs = _songRepository.GetSongsByIds(songIds)
                };

                result.Add(vinylView);
            }

            return result;
        }
    }
}
