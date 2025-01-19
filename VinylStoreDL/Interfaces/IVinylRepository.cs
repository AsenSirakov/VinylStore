using VinylStore.Models.DTO;

namespace VinylStoreDL.Interfaces
{
    public interface IVinylRepository
    {
        List<Vinyl> GetAllVinyls();

        void AddVinyl(Vinyl vinyl);

        Vinyl GetVinylById(string id);

        // New method for deleting a vinyl
        bool DeleteVinylById(string id);
    }
}
