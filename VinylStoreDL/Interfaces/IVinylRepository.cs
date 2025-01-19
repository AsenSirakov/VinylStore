using VinylStore.Models.DTO;

namespace VinylStoreDL.Interfaces
{
    public interface IVinylRepository
    {
        List<Vinyl> GetAllVinyls();

        void AddVinyl(Vinyl vinyl);

        Vinyl GetVinylById(string id);

        
        bool DeleteVinylById(string id);
    }
}
