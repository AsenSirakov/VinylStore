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

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add")]
        public IActionResult AddSong([FromBody] Song song)
        {
            _songService.Add(song);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetSongById(string id)
        {
            var song = _songService.GetById(id);

            if (song == null)
            {
                return NotFound($"Song with ID {id} not found");
            }

            return Ok(song);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAll")]
        public IActionResult GetAllSongs()
        {
            var songs = _songService.GetAll();

            if (songs == null || !songs.Any())
            {
                return NotFound("No songs found");
            }

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
