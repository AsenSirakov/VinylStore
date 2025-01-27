using Moq;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using VinylStoreBL.Services;
using VinylStoreDL.Interfaces;
using Xunit;

public class SongServiceTests
{
    private readonly Mock<ISongRepository> _songRepositoryMock;
    private readonly SongService _songService;

    public SongServiceTests()
    {
        _songRepositoryMock = new Mock<ISongRepository>();
        _songService = new SongService(_songRepositoryMock.Object);
    }

    [Fact]
    public void Add_ShouldCallAddSong_WhenValidSongIsProvided()
    {
        
        var song = new Song { Id = "1", Name = "Song 1" };

        
        _songService.Add(song);

        
        _songRepositoryMock.Verify(repo => repo.AddSong(song), Times.Once);
    }

    [Fact]
    public void GetById_ShouldReturnSong_WhenIdExists()
    {
        
        var song = new Song { Id = "1", Name = "Song 1" };
        _songRepositoryMock.Setup(repo => repo.SongById("1")).Returns(song);

        
        var result = _songService.GetById("1");

        
        Assert.NotNull(result);
        Assert.Equal(song, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        
        _songRepositoryMock.Setup(repo => repo.SongById(It.IsAny<string>())).Returns((Song)null);

        
        var result = _songService.GetById("999");

        
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAllSongs()
    {
        
        var songs = new List<Song>
        {
            new Song { Id = "1", Name = "Song 1" },
            new Song { Id = "2", Name = "Song 2" }
        };
        _songRepositoryMock.Setup(repo => repo.GetAllSongs()).Returns(songs);

        
        var result = _songService.GetAll();

        
        Assert.NotNull(result);
        Assert.Equal(songs.Count, result.Count());
        Assert.Equal(songs, result);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenSongIsDeleted()
    {
        
        _songRepositoryMock.Setup(repo => repo.DeleteSongById("1")).Returns(true);

        
        var result = _songService.Delete("1");

        
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenSongDoesNotExist()
    {
        
        _songRepositoryMock.Setup(repo => repo.DeleteSongById(It.IsAny<string>())).Returns(false);

        
        var result = _songService.Delete("999");

        
        Assert.False(result);
    }

    [Fact]
    public void Update_ShouldReturnTrue_WhenSongIsUpdated()
    {
        
        var updatedSong = new Song { Id = "1", Name = "Updated Song" };
        _songRepositoryMock.Setup(repo => repo.SongById("1")).Returns(new Song { Id = "1", Name = "Old Song" });
        _songRepositoryMock.Setup(repo => repo.UpdateSong(updatedSong)).Returns(true);

        
        var result = _songService.Update(updatedSong);

        
        Assert.True(result);
        _songRepositoryMock.Verify(repo => repo.UpdateSong(updatedSong), Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenSongDoesNotExist()
    {
        
        var updatedSong = new Song { Id = "999", Name = "Nonexistent Song" };
        _songRepositoryMock.Setup(repo => repo.SongById("999")).Returns((Song)null);

        
        var result = _songService.Update(updatedSong);

        
        Assert.False(result);
    }
}
