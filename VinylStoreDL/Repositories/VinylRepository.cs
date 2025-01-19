using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VinylStore.Models.Configurations;
using VinylStore.Models.DTO;
using VinylStoreDL.Interfaces;

namespace VinylStoreDL.Repositories
{
    public class VinylRepository : IVinylRepository
    {
        private readonly IMongoCollection<Vinyl> _vinyls;
        private readonly ILogger<VinylRepository> _logger;

        public VinylRepository(
            IOptionsMonitor<MongoDBConfiguration> mongoConfig,
            ILogger<VinylRepository> logger)
        {
            _logger = logger;
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);

            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);

            _vinyls = database.GetCollection<Vinyl>(
                $"{nameof(Vinyl)}s");
        }

        public List<Vinyl> GetAllVinyls()
        {
            return _vinyls.Find(vinyl => true).ToList();
        }

        public void AddVinyl(Vinyl vinyl)
        {
            if (vinyl == null)
            {
                _logger.LogError("Vinyl is null");
                return;
            }

            try
            {
                vinyl.Id = Guid.NewGuid().ToString();

                _vinyls.InsertOne(vinyl);
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Error adding vinyl {e.Message}-{e.StackTrace}");
            }
        }

        public Vinyl? GetVinylById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            return _vinyls.Find(m => m.Id == id)
                .FirstOrDefault();
        }

        
        public bool DeleteVinylById(string id)
        {
            try
            {
                var result = _vinyls.DeleteOne(vinyl => vinyl.Id == id);
                return result.DeletedCount > 0; 
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Error deleting vinyl with ID {id}: {e.Message}-{e.StackTrace}");
                return false;
            }
        }
    }
}
