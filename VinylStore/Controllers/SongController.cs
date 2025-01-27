using Microsoft.AspNetCore.Mvc;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;

namespace VinylStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly ILogger<SongController> _logger;

        public SongController(ISongService songService, ILogger<SongController> logger)
        {
            _songService = songService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add")]
        public IActionResult AddSong([FromBody] Song song)
        {
            _logger.LogInformation("Attempting to add a song with title: {SongTitle}", song.Name);

            _songService.Add(song);

            _logger.LogInformation("Song with title {SongTitle} was successfully added.", song.Name);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetSongById(string id)
        {
            _logger.LogInformation("Fetching song with ID: {SongId}", id);

            var song = _songService.GetById(id);

            if (song == null)
            {
                _logger.LogWarning("Song with ID {SongId} not found.", id);
                return NotFound($"Song with ID {id} not found");
            }

            _logger.LogInformation("Song with ID {SongId} was found.", id);
            return Ok(song);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAll")]
        public IActionResult GetAllSongs()
        {
            _logger.LogInformation("Fetching all songs.");

            var songs = _songService.GetAll();

            if (songs == null || !songs.Any())
            {
                _logger.LogWarning("No songs found.");
                return NotFound("No songs found");
            }

            _logger.LogInformation("Found {SongCount} songs.", songs.Count());
            return Ok(songs);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteSong(string id)
        {
            var isDeleted = _songService.Delete(id);

            if (!isDeleted)
            {
                return NotFound($"Song with ID {id} not found");
            }

            return Ok($"Song with ID {id} was successfully deleted");
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSong([FromBody] Song updatedSong)
        {
            if (updatedSong == null || string.IsNullOrEmpty(updatedSong.Id))
            {
                return BadRequest("Invalid song data");
            }

            var isUpdated = _songService.Update(updatedSong);

            if (!isUpdated)
            {
                return NotFound($"Song with ID {updatedSong.Id} not found");
            }

            return Ok($"Song with ID {updatedSong.Id} was successfully updated");
        }
    }
}
