using Moq;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using VinylStoreBL.Services;
using VinylStoreDL.Interfaces;
using Xunit;

public class VinylServiceTests
{
    private readonly Mock<IVinylRepository> _vinylRepositoryMock;
    private readonly Mock<ISongRepository> _songRepositoryMock;
    private readonly IVinylService _vinylService;

    public VinylServiceTests()
    {
        _vinylRepositoryMock = new Mock<IVinylRepository>();
        _songRepositoryMock = new Mock<ISongRepository>();
        _vinylService = new VinylService(_vinylRepositoryMock.Object, _songRepositoryMock.Object);
    }

    [Fact]
    public void GetAllVinyls_ReturnsAllVinyls()
    {
        // Arrange
        var vinyls = new List<Vinyl>
        {
            new Vinyl { Id = "1", Name = "Vinyl 1" },
            new Vinyl { Id = "2", Name = "Vinyl 2" }
        };
        _vinylRepositoryMock.Setup(repo => repo.GetAllVinyls()).Returns(vinyls);

        // Act
        var result = _vinylService.GetAllVinyls();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vinyls.Count, result.Count);
        Assert.Equal(vinyls, result);
    }
}
