﻿using Microsoft.Extensions.Logging;
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
        public SongRepository(
            IOptionsMonitor<MongoDBConfiguration> mongoConfig,
            ILogger<SongRepository> logger)
        {
            _logger = logger;
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);

            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);

            _songs = database.GetCollection<Song>(
                $"{nameof(Song)}s");
        }

        public void AddSong(Song song)
        {
            song.Id = System.Guid.NewGuid().ToString();
            
            _songs.InsertOne(song);
        }

     
        public IEnumerable<Song> GetSongsByIds(IEnumerable<string> songsIds)
        {
            var result = _songs.Find(song => songsIds.Contains(song.Id)).ToList();
            return result;
        }

    
        public Song? SongById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            return _songs.Find(m => m.Id == id)
                .FirstOrDefault();
        }
    }
}
