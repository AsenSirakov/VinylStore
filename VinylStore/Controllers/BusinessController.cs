using Microsoft.AspNetCore.Mvc;
using VinylStoreBL.Interfaces;

[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IVinylBlService _vinylService;
    private readonly ILogger<BusinessController> _logger;
    public BusinessController(IVinylBlService vinylService, ILogger<BusinessController> logger)
    {
        _vinylService = vinylService;
        _logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("GetAllVinylWithDetails")]
    public IActionResult GetAllVinylWithDetails()
    {
        _logger.LogInformation("Attempting to fetch all vinyls with details.");

        var result = _vinylService.GetDetailedVinyls();

        if (result == null || result.Count == 0)
        {
            _logger.LogWarning("No vinyls found.");
            return NotFound("No vinyls found");
        }

        _logger.LogInformation("Successfully fetched {Count} vinyls.", result.Count);
        return Ok(result); ;
    }


    [HttpGet("SearchVinylBySongName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult SearchVinylBySongName(string songName)
    {
        if (string.IsNullOrWhiteSpace(songName))
        {
            _logger.LogWarning("Song name cannot be null or empty.");
            return BadRequest("Song name cannot be null or empty");
        }

        _logger.LogInformation("Searching for vinyls containing the song '{SongName}'.", songName);
        var result = _vinylService.GetVinylsBySongName(songName);

        if (result == null || !result.Any())
        {
            _logger.LogWarning("No vinyls found containing the song '{SongName}'.", songName);
            return NotFound($"No vinyls found containing the song '{songName}'");
        }

        _logger.LogInformation("Found {Count} vinyls containing the song '{SongName}'.", result.Count, songName);
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
            _logger.LogWarning("Genre cannot be null or empty.");
            return BadRequest("Genre cannot be null or empty");
        }

        _logger.LogInformation("Fetching vinyls with songs in the genre '{Genre}'.", genre);
        var result = _vinylService.GetVinylsBySongGenre(genre);

        if (result == null || !result.Any())
        {
            _logger.LogWarning("No vinyls found with songs in the genre '{Genre}'.", genre);
            return NotFound($"No vinyls found with songs in the genre '{genre}'");
        }

        _logger.LogInformation("Found {Count} vinyls with songs in the genre '{Genre}'.", result.Count, genre);
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
            _logger.LogWarning("Artist name cannot be null or empty.");
            return BadRequest("Artist name cannot be null or empty");
        }

        _logger.LogInformation("Fetching vinyls with songs by the artist '{Artist}'.", artist);
        var result = _vinylService.GetVinylsBySongArtist(artist);

        if (result == null || !result.Any())
        {
            _logger.LogWarning("No vinyls found with songs by the artist '{Artist}'.", artist);
            return NotFound($"No vinyls found with songs by the artist '{artist}'");
        }

        _logger.LogInformation("Found {Count} vinyls with songs by the artist '{Artist}'.", result.Count, artist);
        return Ok(result);
    }


}
