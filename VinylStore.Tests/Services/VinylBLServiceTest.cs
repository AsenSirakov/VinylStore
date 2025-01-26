using Moq;
using System.Collections.Generic;
using System.Linq;
using VinylStore.Models.DTO;
using VinylStore.Models.Views;
using VinylStoreBL.Interfaces;
using VinylStoreDL.Interfaces;
using Xunit;

public class VinylBlServiceInterfaceTests
{
    private readonly Mock<IVinylService> _vinylServiceMock;
    private readonly Mock<ISongRepository> _songRepositoryMock;
    private readonly Mock<IVinylBlService> _vinylBlServiceMock;

    public VinylBlServiceInterfaceTests()
    {
        _vinylServiceMock = new Mock<IVinylService>();
        _songRepositoryMock = new Mock<ISongRepository>();
        _vinylBlServiceMock = new Mock<IVinylBlService>();
    }

    [Fact]
    public void GetDetailedVinyls_ReturnsVinylsWithDetails()
    {
        // Arrange
        var vinylViews = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" },
            new VinylView { VinylId = "2", VinylName = "Vinyl 2" }
        };

        _vinylBlServiceMock.Setup(service => service.GetDetailedVinyls()).Returns(vinylViews);

        // Act
        var result = _vinylBlServiceMock.Object.GetDetailedVinyls();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetVinylsBySongName_ReturnsMatchingVinyls()
    {
        // Arrange
        var vinylViews = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };

        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongName("Song 1")).Returns(vinylViews);

        // Act
        var result = _vinylBlServiceMock.Object.GetVinylsBySongName("Song 1");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Vinyl 1", result.First().VinylName);
    }

    [Fact]
    public void GetVinylsBySongGenre_ReturnsMatchingVinyls()
    {
        // Arrange
        var vinylViews = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };

        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongGenre("Rock")).Returns(vinylViews);

        // Act
        var result = _vinylBlServiceMock.Object.GetVinylsBySongGenre("Rock");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Vinyl 1", result.First().VinylName);
    }

    [Fact]
    public void GetVinylsBySongArtist_ReturnsMatchingVinyls()
    {
        // Arrange
        var vinylViews = new List<VinylView>
        {
            new VinylView { VinylId = "1", VinylName = "Vinyl 1" }
        };

        _vinylBlServiceMock.Setup(service => service.GetVinylsBySongArtist("Artist 1")).Returns(vinylViews);

        // Act
        var result = _vinylBlServiceMock.Object.GetVinylsBySongArtist("Artist 1");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Vinyl 1", result.First().VinylName);
    }
}
