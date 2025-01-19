using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Models.DTO;
using VinylStore.Models.Requests;
using VinylStoreBL.Interfaces;

namespace VinylStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VinylsController : ControllerBase
    {
        private readonly IVinylService _vinylService;
        private readonly IMapper _mapper;
        private readonly ILogger<VinylsController> _logger;

        public VinylsController(
            IVinylService vinylService, IMapper mapper, ILogger<VinylsController> logger)
        {
            _vinylService = vinylService;
            _mapper = mapper;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _vinylService.GetAllVinyls();

            if (result == null || result.Count == 0)
            {
                return NotFound("No vinyls found");
            }

            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id can't be null or empty");
            }

            var result = _vinylService.GetVinylById(id);

            if (result == null)
            {
                return NotFound($"Vinyl with ID:{id} not found");
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(AddVinylRequest vinyl)
        {
            try
            {
                var vinylDto = _mapper.Map<Vinyl>(vinyl);

                if (vinylDto == null)
                {
                    return BadRequest("Can't convert vinyl to vinyl DTO");
                }

                _vinylService.AddVinyl(vinylDto);

                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error adding vinyl");
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id can't be null or empty");
            }

            var isDeleted = _vinylService.DeleteVinylById(id);

            if (!isDeleted)
            {
                return NotFound($"Vinyl with ID:{id} not found");
            }

            return Ok($"Vinyl with ID:{id} successfully deleted");
        }
    }
}
