using Microsoft.AspNetCore.Mvc;
using MovieApi.Controllers;
using MovieApi.Helpers;
using Xunit;
using Moq;

namespace MovieApi.Tests;

public class TestMovieApi
{
    private IUtils CreateFileUtilsMock()
    {
        List<Movie> movies = new List<Movie>
        {
            new("testName",
                "testDescription",
                1992,
                "testGenre"
            ),
            new("testName2",
                "newDescription",
                2002,
                "testGenre"
            ),
            new("testName3",
                "unitDescription",
                1992,
                "testGenre"
            ),
        };
        
        var mock = new Mock<IUtils>();

        mock.Setup(fu => fu.LoadMovies()).Returns(movies);
        mock.Setup(fu => fu.SaveMovies(It.IsAny<List<Movie>>())).Callback((List<Movie> m) => movies = m);
        
        return mock.Object;
    }
    
    [Fact]
    public void GetMovie_FileMissing_BadResult()
    {
        var controller = new MovieController(new List<Movie>());
        var result = controller.GetMovie();
        Assert.IsType<BadRequestResult>(result.Result);
    }
    
    [Fact]
    public void GetMovie_FileExists_OkResult()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetMovie();
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetFromYear_FoundMatching_ReturnMatching()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetFromYear(1992);
        Assert.Equal(2, ((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList().Count);
    }
    
    [Fact]
    public void GetFromYear_NotFoundMatching_ReturnEmpty()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetFromYear(1993);
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }
    
    [Fact]
    public void GetFromGenre_FoundMatching_ReturnMatching()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetFromGenre("testGenre");
        Assert.Equal(3, ((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList().Count);
    }
    
    [Fact]
    public void GetFromGenre_NotFoundMatching_ReturnEmpty()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetFromGenre("action");
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }

    [Fact]
    public void AddMovie_GoodData_Added()
    {
        var fileUtils = CreateFileUtilsMock();
        var controller = new MovieController(fileUtils.LoadMovies());
        var result = controller.GetFromYear(2000);
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
        controller.AddMovie(new Movie("newName", "newDescription", 2000, "newGenre"));
        result = controller.GetFromYear(2000);
        Assert.Single(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }
}