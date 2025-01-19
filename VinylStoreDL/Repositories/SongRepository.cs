using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VinylStore.Models.Configurations;
using VinylStore.Models.DTO;
using VinylStoreDL.Interfaces;

namespace VinylStoreDL.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly IMongoCollection<Song> _songs;
        private readonly ILogger<SongRepository> _logger;

        public SongRepository(IOptionsMonitor<MongoDBConfiguration> mongoConfig, ILogger<SongRepository> logger)
        {
            _logger = logger;
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _songs = database.GetCollection<Song>($"{nameof(Song)}s");
        }

        public void AddSong(Song song)
        {
            song.Id = Guid.NewGuid().ToString();
            _songs.InsertOne(song);
        }

        public IEnumerable<Song> GetSongsByIds(IEnumerable<string> songsIds)
        {
            return _songs.Find(song => songsIds.Contains(song.Id)).ToList();
        }

        public Song? SongById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            return _songs.Find(m => m.Id == id).FirstOrDefault();
        }

        
        public IEnumerable<Song> GetAllSongs()
        {
            return _songs.Find(song => true).ToList();
        }

        public bool DeleteSongById(string id)
        {
            var result = _songs.DeleteOne(song => song.Id == id);
            return result.DeletedCount > 0; // Return true if a song was deleted
        }

    }
}
