using Moq;
using System.Collections.Generic;
using System.Linq;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using Xunit;

public class SongServiceInterfaceTests
{
    private readonly Mock<ISongService> _songServiceMock;

    public SongServiceInterfaceTests()
    {
        _songServiceMock = new Mock<ISongService>();
    }

    [Fact]
    public void Add_ShouldInvokeAddMethod()
    {
        // Arrange
        var song = new Song { Id = "1", Name = "Song 1" };

        // Act
        _songServiceMock.Object.Add(song);

        // Assert
        _songServiceMock.Verify(service => service.Add(song), Times.Once);
    }

    [Fact]
    public void GetById_ShouldReturnSong_WhenIdExists()
    {
        // Arrange
        var song = new Song { Id = "1", Name = "Song 1" };
        _songServiceMock.Setup(service => service.GetById("1")).Returns(song);

        // Act
        var result = _songServiceMock.Object.GetById("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Song 1", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        _songServiceMock.Setup(service => service.GetById("999")).Returns((Song)null);

        // Act
        var result = _songServiceMock.Object.GetById("999");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAllSongs()
    {
        // Arrange
        var songs = new List<Song>
        {
            new Song { Id = "1", Name = "Song 1" },
            new Song { Id = "2", Name = "Song 2" }
        };
        _songServiceMock.Setup(service => service.GetAll()).Returns(songs);

        // Act
        var result = _songServiceMock.Object.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenSongExists()
    {
        // Arrange
        _songServiceMock.Setup(service => service.Delete("1")).Returns(true);

        // Act
        var result = _songServiceMock.Object.Delete("1");

        // Assert
        Assert.True(result);
        _songServiceMock.Verify(service => service.Delete("1"), Times.Once);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenSongDoesNotExist()
    {
        // Arrange
        _songServiceMock.Setup(service => service.Delete("999")).Returns(false);

        // Act
        var result = _songServiceMock.Object.Delete("999");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Update_ShouldReturnTrue_WhenSongIsUpdated()
    {
        // Arrange
        var updatedSong = new Song { Id = "1", Name = "Updated Song" };
        _songServiceMock.Setup(service => service.Update(updatedSong)).Returns(true);

        // Act
        var result = _songServiceMock.Object.Update(updatedSong);

        // Assert
        Assert.True(result);
        _songServiceMock.Verify(service => service.Update(updatedSong), Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenSongDoesNotExist()
    {
        // Arrange
        var updatedSong = new Song { Id = "999", Name = "Nonexistent Song" };
        _songServiceMock.Setup(service => service.Update(updatedSong)).Returns(false);

        // Act
        var result = _songServiceMock.Object.Update(updatedSong);

        // Assert
        Assert.False(result);
    }
}
