using Microsoft.AspNetCore.Mvc;
using VinylStoreBL.Interfaces;

[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IVinylBlService _vinylService;

    public BusinessController(IVinylBlService vinylService)
    {
        _vinylService = vinylService;
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


    [HttpGet("SearchVinylBySongName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult SearchVinylBySongName(string songName)
    {
        if (string.IsNullOrWhiteSpace(songName))
        {
            return BadRequest("Song name cannot be null or empty");
        }

        var result = _vinylService.GetVinylsBySongName(songName);

        if (result == null || !result.Any())
        {
            return NotFound($"No vinyls found containing the song '{songName}'");
        }

        return Ok(result);
    }

    [HttpGet("GetVinylsBySongGenre/{genre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetVinylsBySongGenre(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            return BadRequest("Genre cannot be null or empty");
        }

        var result = _vinylService.GetVinylsBySongGenre(genre);

        if (result == null || !result.Any())
        {
            return NotFound($"No vinyls found with songs in the genre '{genre}'");
        }

        return Ok(result);
    }

    [HttpGet("GetVinylsBySongArtist/{artist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetVinylsBySongArtist(string artist)
    {
        if (string.IsNullOrWhiteSpace(artist))
        {
            return BadRequest("Artist name cannot be null or empty");
        }

        var result = _vinylService.GetVinylsBySongArtist(artist);

        if (result == null || !result.Any())
        {
            return NotFound($"No vinyls found with songs by the artist '{artist}'");
        }

        return Ok(result);
    }


}
