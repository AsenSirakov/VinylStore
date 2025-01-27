using Moq;
using VinylStore.Models.DTO;
using VinylStoreBL.Services;
using VinylStoreDL.Interfaces;
using Xunit;

public class VinylServiceTests
{
    private readonly Mock<IVinylRepository> _vinylRepositoryMock;
    private readonly Mock<ISongRepository> _songRepositoryMock;
    private readonly VinylService _vinylService;

    public VinylServiceTests()
    {
        _vinylRepositoryMock = new Mock<IVinylRepository>();
        _songRepositoryMock = new Mock<ISongRepository>();
        _vinylService = new VinylService(_vinylRepositoryMock.Object, _songRepositoryMock.Object);
    }

    [Fact]
    public void GetAllVinyls_ShouldReturnAllVinyls()
    {
        
        var vinyls = new List<Vinyl>
        {
            new Vinyl { Id = "1", Name = "Classic Rock" },
            new Vinyl { Id = "2", Name = "Pop Hits" }
        };

        _vinylRepositoryMock.Setup(repo => repo.GetAllVinyls()).Returns(vinyls);

       
        var result = _vinylService.GetAllVinyls();

       
        Assert.NotNull(result);
        Assert.Equal(vinyls.Count, result.Count);
    }

    [Fact]
    public void AddVinyl_ShouldAddVinyl_WhenAllSongsExist()
    {
        
        var vinyl = new Vinyl
        {
            Id = "1",
            Name = "Classic Rock",
            Songs = new List<Song>
            {
                new Song { Id = "101" },
                new Song { Id = "102" }
            }
        };

        _songRepositoryMock.Setup(repo => repo.SongById(It.IsAny<string>())).Returns(new Song());

        
        _vinylService.AddVinyl(vinyl);

        
        _vinylRepositoryMock.Verify(repo => repo.AddVinyl(vinyl), Times.Once);
    }

    [Fact]
    public void AddVinyl_ShouldThrowException_WhenSongDoesNotExist()
    {
        
        var vinyl = new Vinyl
        {
            Id = "1",
            Name = "Classic Rock",
            Songs = new List<Song>
            {
                new Song { Id = "999" }
            }
        };

        _songRepositoryMock.Setup(repo => repo.SongById("999")).Returns((Song)null);

        
        var exception = Assert.Throws<Exception>(() => _vinylService.AddVinyl(vinyl));
        Assert.Equal("Song with id 999 does not exist", exception.Message);
    }

    [Fact]
    public void GetVinylById_ShouldReturnVinyl_WhenIdExists()
    {
        
        var vinyl = new Vinyl { Id = "1", Name = "Classic Rock" };

        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("1")).Returns(vinyl);

        
        var result = _vinylService.GetVinylById("1");

        
        Assert.NotNull(result);
        Assert.Equal(vinyl, result);
    }

    [Fact]
    public void GetVinylById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        
        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("999")).Returns((Vinyl)null);

        
        var result = _vinylService.GetVinylById("999");

        
        Assert.Null(result);
    }

    [Fact]
    public void DeleteVinylById_ShouldReturnTrue_WhenVinylExists()
    {
        
        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("1")).Returns(new Vinyl());
        _vinylRepositoryMock.Setup(repo => repo.DeleteVinylById("1")).Returns(true);

        
        var result = _vinylService.DeleteVinylById("1");

        
        Assert.True(result);
    }

    [Fact]
    public void DeleteVinylById_ShouldReturnFalse_WhenVinylDoesNotExist()
    {
        
        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("999")).Returns((Vinyl)null);

        
        var result = _vinylService.DeleteVinylById("999");

        
        Assert.False(result);
    }

    [Fact]
    public void UpdateVinyl_ShouldReturnTrue_WhenVinylExists()
    {
        
        var updatedVinyl = new Vinyl { Id = "1", Name = "Updated Vinyl" };
        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("1")).Returns(new Vinyl());
        _vinylRepositoryMock.Setup(repo => repo.UpdateVinyl(updatedVinyl)).Returns(true);

        
        var result = _vinylService.UpdateVinyl(updatedVinyl);

        
        Assert.True(result);
    }

    [Fact]
    public void UpdateVinyl_ShouldReturnFalse_WhenVinylDoesNotExist()
    {
        
        var updatedVinyl = new Vinyl { Id = "999", Name = "Nonexistent Vinyl" };
        _vinylRepositoryMock.Setup(repo => repo.GetVinylById("999")).Returns((Vinyl)null);

        
        var result = _vinylService.UpdateVinyl(updatedVinyl);

        
        Assert.False(result);
    }
}
