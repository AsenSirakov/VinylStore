using Microsoft.AspNetCore.Mvc;
using Moq;
using VinylStore.Models.Views;
using VinylStoreBL.Interfaces;
using Xunit;

public class BusinessControllerTests
{
    private readonly Mock<IVinylBlService> _vinylBlServiceMock;
    private readonly BusinessController _controller;

    public BusinessControllerTests()
    {
        _vinylBlServiceMock = new Mock<IVinylBlService>();
        _controller = new BusinessController(_vinylBlServiceMock.Object);
    }

    [Fact]
    public void GetAllVinylWithDetails_ReturnsOkResult_WhenVinylsExist()
    {
        // Arrange
        var vinyls = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" },
            new VinylView { VinylId = "2", VinylName = "Vinyl 2" }
        };
        _vinylBlServiceMock.Setup(service => service.GetDetailedVinyls()).Returns(vinyls);

        // Act
        var result = _controller.GetAllVinylWithDetails();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<VinylView>>(okResult.Value);
        Assert.Equal(vinyls.Count, returnValue.Count);
    }

    [Fact]
    public void GetAllVinylWithDetails_ReturnsNotFound_WhenNoVinylsExist()
    {
        // Arrange
        _vinylBlServiceMock.Setup(service => service.GetDetailedVinyls()).Returns(new List<VinylView>());

        // Act
        var result = _controller.GetAllVinylWithDetails();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void SearchVinylBySongName_ReturnsOkResult_WhenMatchesAreFound()
    {
        // Arrange
        var vinyls = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongName("Song 1")).Returns(vinyls);

        // Act
        var result = _controller.SearchVinylBySongName("Song 1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<VinylView>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public void SearchVinylBySongName_ReturnsNotFound_WhenNoMatchesAreFound()
    {
        // Arrange
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongName("Nonexistent Song")).Returns(new List<VinylView>());

        // Act
        var result = _controller.SearchVinylBySongName("Nonexistent Song");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void SearchVinylBySongName_ReturnsBadRequest_WhenSongNameIsEmpty()
    {
        // Act
        var result = _controller.SearchVinylBySongName("");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetVinylsBySongGenre_ReturnsOkResult_WhenMatchesAreFound()
    {
        // Arrange
        var vinyls = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongGenre("Rock")).Returns(vinyls);

        // Act
        var result = _controller.GetVinylsBySongGenre("Rock");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<VinylView>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public void GetVinylsBySongGenre_ReturnsNotFound_WhenNoMatchesAreFound()
    {
        // Arrange
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongGenre("Classical")).Returns(new List<VinylView>());

        // Act
        var result = _controller.GetVinylsBySongGenre("Classical");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetVinylsBySongGenre_ReturnsBadRequest_WhenGenreIsEmpty()
    {
        // Act
        var result = _controller.GetVinylsBySongGenre("");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetVinylsBySongArtist_ReturnsOkResult_WhenMatchesAreFound()
    {
        // Arrange
        var vinyls = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongArtist("Artist 1")).Returns(vinyls);

        // Act
        var result = _controller.GetVinylsBySongArtist("Artist 1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<VinylView>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public void GetVinylsBySongArtist_ReturnsNotFound_WhenNoMatchesAreFound()
    {
        // Arrange
        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongArtist("Unknown Artist")).Returns(new List<VinylView>());

        // Act
        var result = _controller.GetVinylsBySongArtist("Unknown Artist");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetVinylsBySongArtist_ReturnsBadRequest_WhenArtistIsEmpty()
    {
        // Act
        var result = _controller.GetVinylsBySongArtist("");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
