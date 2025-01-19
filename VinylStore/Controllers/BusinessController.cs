using Microsoft.AspNetCore.Mvc;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;

namespace VinylStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IVinylBlService _vinylService;
        private readonly ISongService _songService;

        public BusinessController(IVinylBlService vinylService, ISongService songService)
        {
            _vinylService = vinylService;
            _songService = songService;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllVinylWithDetails")]
        public IActionResult GetAllVinylWithDetails()
        {
            var result = _vinylService.GetDetailedVinyls();

            if (result == null || result.Count == 0)
            {
                return NotFound("No vinyls found");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddSong")]
        public IActionResult AddSong([FromBody] Song song)
        {
            _songService.Add(song);

            return Ok();
        }

        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetSongById/{id}")]
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
        [HttpGet("GetAllSongs")]
        public IActionResult GetAllSongs()
        {
            var songs = _songService.GetAll();

            if (songs == null || !songs.Any())
            {
                return NotFound("No songs found");
            }

            return Ok(songs);
        }
    }
}
