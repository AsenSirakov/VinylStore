using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VinylStore.Controllers;
using VinylStore.Models.DTO;
using VinylStore.Models.Requests;
using VinylStoreBL.Interfaces;
using Xunit;

public class VinylsControllerTests
{
    private readonly Mock<IVinylService> _vinylServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<VinylsController>> _loggerMock;
    private readonly VinylsController _controller;

    public VinylsControllerTests()
    {
        _vinylServiceMock = new Mock<IVinylService>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<VinylsController>>();
        _controller = new VinylsController(_vinylServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void Get_ReturnsOkResult_WhenVinylsExist()
    {
        // Arrange
        var vinyls = new List<Vinyl>
        {
            new Vinyl { Id = "1", Name = "Vinyl 1" },
            new Vinyl { Id = "2", Name = "Vinyl 2" }
        };
        _vinylServiceMock.Setup(service => service.GetAllVinyls()).Returns(vinyls);

        // Act
        var result = _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Vinyl>>(okResult.Value);
        Assert.Equal(vinyls.Count, returnValue.Count);
    }

    [Fact]
    public void Get_ReturnsNotFound_WhenNoVinylsExist()
    {
        // Arrange
        _vinylServiceMock.Setup(service => service.GetAllVinyls()).Returns(new List<Vinyl>());

        // Act
        var result = _controller.Get();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetById_ReturnsOkResult_WhenVinylIsFound()
    {
        // Arrange
        var vinyl = new Vinyl { Id = "1", Name = "Vinyl 1" };
        _vinylServiceMock.Setup(service => service.GetVinylById("1")).Returns(vinyl);

        // Act
        var result = _controller.GetById("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(vinyl, okResult.Value);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenVinylIsNotFound()
    {
        // Arrange
        _vinylServiceMock.Setup(service => service.GetVinylById("999")).Returns((Vinyl)null);

        // Act
        var result = _controller.GetById("999");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetById_ReturnsBadRequest_WhenIdIsNullOrEmpty()
    {
        // Act
        var result = _controller.GetById("");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Add_ReturnsOkResult_WhenVinylIsAddedSuccessfully()
    {
        // Arrange
        var request = new AddVinylRequest { Name = "New Vinyl" };
        var vinyl = new Vinyl { Id = "1", Name = "New Vinyl" };

        _mapperMock.Setup(mapper => mapper.Map<Vinyl>(request)).Returns(vinyl);

        // Act
        var result = _controller.Add(request);

        // Assert
        Assert.IsType<OkResult>(result);
        _vinylServiceMock.Verify(service => service.AddVinyl(vinyl), Times.Once);
    }

    [Fact]
    public void Add_ReturnsBadRequest_WhenMapperFails()
    {
        // Arrange
        var request = new AddVinylRequest { Name = "New Vinyl" };
        _mapperMock.Setup(mapper => mapper.Map<Vinyl>(request)).Returns((Vinyl)null);

        // Act
        var result = _controller.Add(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsOkResult_WhenVinylIsDeleted()
    {
        // Arrange
        _vinylServiceMock.Setup(service => service.DeleteVinylById("1")).Returns(true);

        // Act
        var result = _controller.Delete("1");

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenVinylDoesNotExist()
    {
        // Arrange
        _vinylServiceMock.Setup(service => service.DeleteVinylById("999")).Returns(false);

        // Act
        var result = _controller.Delete("999");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsBadRequest_WhenIdIsNullOrEmpty()
    {
        // Act
        var result = _controller.Delete("");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Update_ReturnsOkResult_WhenVinylIsUpdated()
    {
        // Arrange
        var vinyl = new Vinyl { Id = "1", Name = "Updated Vinyl" };
        _vinylServiceMock.Setup(service => service.UpdateVinyl(vinyl)).Returns(true);

        // Act
        var result = _controller.UpdateVinyl(vinyl);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Update_ReturnsNotFound_WhenVinylDoesNotExist()
    {
        // Arrange
        var vinyl = new Vinyl { Id = "999", Name = "Nonexistent Vinyl" };
        _vinylServiceMock.Setup(service => service.UpdateVinyl(vinyl)).Returns(false);

        // Act
        var result = _controller.UpdateVinyl(vinyl);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenVinylIsInvalid()
    {
        // Act
        var result = _controller.UpdateVinyl(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
