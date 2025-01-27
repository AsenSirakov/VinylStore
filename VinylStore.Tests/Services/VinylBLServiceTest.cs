using Moq;
using VinylStore.Models.DTO;
using VinylStore.Models.Views;
using VinylStoreBL.Interfaces;
using VinylStoreBL.Services;
using VinylStoreDL.Interfaces;
using Xunit;

public class VinylBlServiceTests
{
    private readonly Mock<IVinylService> _vinylServiceMock;
    private readonly Mock<ISongRepository> _songRepositoryMock;
    private readonly VinylBlService _vinylBlService;

    public VinylBlServiceTests()
    {
        _vinylServiceMock = new Mock<IVinylService>();
        _songRepositoryMock = new Mock<ISongRepository>();

        _vinylBlService = new VinylBlService(
            _vinylServiceMock.Object,
            _songRepositoryMock.Object
        );
    }

    [Fact]
    public void GetDetailedVinyls_ShouldReturnDetailedVinyls_WhenDataExists()
    {
        
        var vinyls = new List<Vinyl>
        {
            new Vinyl
            {
                Id = "1",
                Name = "Classic Rock",
                Songs = new List<Song>
                {
                    new Song { Id = "101" },
                    new Song { Id = "102" }
                }
            }
        };

        var songs = new List<Song>
        {
            new Song { Id = "101", Name = "Bohemian Rhapsody" },
            new Song { Id = "102", Name = "Hotel California" }
        };

        _vinylServiceMock
            .Setup(service => service.GetAllVinyls())
            .Returns(vinyls);

        _songRepositoryMock
            .Setup(repo => repo.GetSongsByIds(It.IsAny<IEnumerable<string>>()))
            .Returns(songs);

        
        var result = _vinylBlService.GetDetailedVinyls();

        
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Classic Rock", result[0].VinylName);
        Assert.Equal(2, result[0].Songs.Count());
        Assert.Contains(result[0].Songs, s => s.Name == "Bohemian Rhapsody");
        Assert.Contains(result[0].Songs, s => s.Name == "Hotel California");
    }

    [Fact]
    public void GetDetailedVinyls_ShouldReturnEmptyList_WhenNoVinylsExist()
    {
        
        _vinylServiceMock
            .Setup(service => service.GetAllVinyls())
            .Returns(new List<Vinyl>());

        
        var result = _vinylBlService.GetDetailedVinyls();

       
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetDetailedVinyls_ShouldReturnVinylsWithEmptySongs_WhenSongsNotFound()
    {
        
        var vinyls = new List<Vinyl>
        {
            new Vinyl
            {
                Id = "1",
                Name = "Classic Rock",
                Songs = new List<Song>
                {
                    new Song { Id = "101" },
                    new Song { Id = "102" }
                }
            }
        };

        _vinylServiceMock
            .Setup(service => service.GetAllVinyls())
            .Returns(vinyls);

        _songRepositoryMock
            .Setup(repo => repo.GetSongsByIds(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<Song>()); 

        
        var result = _vinylBlService.GetDetailedVinyls();

        
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Empty(result[0].Songs);
    }
}
