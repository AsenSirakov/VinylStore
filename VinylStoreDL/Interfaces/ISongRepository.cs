﻿using VinylStore.Models.DTO;

namespace VinylStoreDL.Interfaces
{
    public interface ISongRepository
    {
        void AddSong(Song song);

        IEnumerable<Song> GetSongsByIds(IEnumerable<string> songIds);

        Song? SongById(string id);

        // New method to retrieve all songs
        IEnumerable<Song> GetAllSongs();
    }
}
