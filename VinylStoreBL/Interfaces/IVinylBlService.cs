using VinylStore.Models.Views;

namespace VinylStoreBL.Interfaces
{
    public interface IVinylBlService
    {
        List<VinylView> GetDetailedVinyls();
    }
}
