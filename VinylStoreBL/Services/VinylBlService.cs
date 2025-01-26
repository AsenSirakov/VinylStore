using VinylStore.Models.DTO;
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


        public List<VinylView> GetVinylsBySongName(string songName)
        {
            var allSongs = _songRepository.GetAllSongs()
                                          .Where(s => s.Name.Equals(songName, StringComparison.OrdinalIgnoreCase))
                                          .Select(s => s.Id)
                                          .ToList();

            var vinyls = _vinylService.GetAllVinyls()
                                      .Where(v => v.Songs.Any(s => allSongs.Contains(s.Id)))
                                      .ToList();

            var result = new List<VinylView>();

            foreach (var vinyl in vinyls)
            {
                List<string> songIds = vinyl.Songs.Select(song => song.Id).ToList();

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

        public List<VinylView> GetVinylsBySongGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                throw new ArgumentException("Genre cannot be null or empty", nameof(genre));
            }

            var allSongs = _songRepository.GetAllSongs()
                                          ?.Where(s => s.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                                          .Select(s => s.Id)
                                          .ToList() ?? new List<string>();

            if (!allSongs.Any())
            {
                return new List<VinylView>();
            }

            var vinyls = _vinylService.GetAllVinyls()
                                      ?.Where(v => v.Songs.Any(s => allSongs.Contains(s.Id)))
                                      .ToList() ?? new List<Vinyl>();

            var result = new List<VinylView>();

            foreach (var vinyl in vinyls)
            {
                var songIds = vinyl.Songs.Select(song => song.Id).ToList();

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


        public List<VinylView> GetVinylsBySongArtist(string artist)
        {
            if (string.IsNullOrWhiteSpace(artist))
            {
                throw new ArgumentException("Artist name cannot be null or empty", nameof(artist));
            }

            var allSongs = _songRepository.GetAllSongs()
                                          ?.Where(s => s.Singer != null && s.Singer.Name.Equals(artist, StringComparison.OrdinalIgnoreCase))
                                          .Select(s => s.Id)
                                          .ToList() ?? new List<string>();

            if (!allSongs.Any())
            {
                return new List<VinylView>();
            }

            var vinyls = _vinylService.GetAllVinyls()
                                      ?.Where(v => v.Songs.Any(s => allSongs.Contains(s.Id)))
                                      .ToList() ?? new List<Vinyl>();

            var result = new List<VinylView>();

            foreach (var vinyl in vinyls)
            {
                var songIds = vinyl.Songs.Select(song => song.Id).ToList();

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
