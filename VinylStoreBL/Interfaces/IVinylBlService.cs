using VinylStore.Models.Views;

namespace VinylStoreBL.Interfaces
{
    public interface IVinylBlService
    {
        List<VinylView> GetDetailedVinyls();

        List<VinylView> GetVinylsBySongName(string songName);

        List<VinylView> GetVinylsBySongGenre(string genre);
        List<VinylView> GetVinylsBySongArtist(string artist);


    }
}
