using VinylStore.Models.DTO;

namespace VinylStoreBL.Interfaces
{
    public interface IVinylService
    {
        List<Vinyl> GetAllVinyls();

        void AddVinyl(Vinyl vinyl);

        Vinyl? GetVinylById(string id);
    }
}
