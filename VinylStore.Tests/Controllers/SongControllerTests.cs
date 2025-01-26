using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using VinylStore.Controllers;
using VinylStore.Models.DTO;
using VinylStoreBL.Interfaces;
using Xunit;

public class SongControllerTests
{
    private readonly Mock<ISongService> _songServiceMock;
    private readonly SongController _songController;

    public SongControllerTests()
    {
        _songServiceMock = new Mock<ISongService>();
        _songController = new SongController(_songServiceMock.Object);
    }

    [Fact]
    public void AddSong_ShouldCallServiceAndReturnOk()
    {
        // Arrange
        var song = new Song { Id = "1", Name = "Song 1" };

        // Act
        var result = _songController.AddSong(song);

        // Assert
        _songServiceMock.Verify(service => service.Add(song), Times.Once);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void GetSongById_ShouldReturnSong_WhenExists()
    {
        // Arrange
        var song = new Song { Id = "1", Name = "Song 1" };
        _songServiceMock.Setup(service => service.GetById("1")).Returns(song);

        // Act
        var result = _songController.GetSongById("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Song>(okResult.Value);
        Assert.Equal("Song 1", returnValue.Name);
    }

    [Fact]
    public void GetSongById_ShouldReturnNotFound_WhenNotExists()
    {
        // Arrange
        _songServiceMock.Setup(service => service.GetById("999")).Returns((Song)null);

        // Act
        var result = _songController.GetSongById("999");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetAllSongs_ShouldReturnAllSongs_WhenExists()
    {
        // Arrange
        var songs = new List<Song>
        {
            new Song { Id = "1", Name = "Song 1" },
            new Song { Id = "2", Name = "Song 2" }
        };
        _songServiceMock.Setup(service => service.GetAll()).Returns(songs);

        // Act
        var result = _songController.GetAllSongs();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Song>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public void GetAllSongs_ShouldReturnNotFound_WhenNoSongsExist()
    {
        // Arrange
        _songServiceMock.Setup(service => service.GetAll()).Returns(new List<Song>());

        // Act
        var result = _songController.GetAllSongs();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeleteSong_ShouldReturnOk_WhenSongIsDeleted()
    {
        // Arrange
        _songServiceMock.Setup(service => service.Delete("1")).Returns(true);

        // Act
        var result = _songController.DeleteSong("1");

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void DeleteSong_ShouldReturnNotFound_WhenSongDoesNotExist()
    {
        // Arrange
        _songServiceMock.Setup(service => service.Delete("999")).Returns(false);

        // Act
        var result = _songController.DeleteSong("999");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void UpdateSong_ShouldReturnOk_WhenSongIsUpdated()
    {
        // Arrange
        var song = new Song { Id = "1", Name = "Updated Song" };
        _songServiceMock.Setup(service => service.Update(song)).Returns(true);

        // Act
        var result = _songController.UpdateSong(song);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void UpdateSong_ShouldReturnNotFound_WhenSongDoesNotExist()
    {
        // Arrange
        var song = new Song { Id = "999", Name = "Nonexistent Song" };
        _songServiceMock.Setup(service => service.Update(song)).Returns(false);

        // Act
        var result = _songController.UpdateSong(song);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void UpdateSong_ShouldReturnBadRequest_WhenSongDataIsInvalid()
    {
        // Arrange
        Song song = null;

        // Act
        var result = _songController.UpdateSong(song);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
