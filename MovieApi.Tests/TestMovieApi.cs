using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Controllers;
using MovieApi.Helpers;
using Xunit;

namespace MovieApi.Tests;

[Collection("MovieApi")]
public class TestMovieApi
{
    [Fact]
    public void GetMovie_FileMissing_BadResult()
    {
        try
        {
            File.Delete("movies.json");
        }
        catch (Exception)
        {
            // ignored
        }

        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetMovie();
        Assert.IsType<BadRequestResult>(result.Result);
    }
    
    [Fact]
    public void GetMovie_FileExists_OkResult()
    {
        List<Movie> movies = new List<Movie>
        {
            new("testName",
                "testDescription",
                1990,
                "testGenre"
            ),
        };
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetMovie();
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetFromYear_FoundMatching_ReturnMatching()
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
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetFromYear(1992);
        Assert.Equal(2, ((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList().Count);
    }
    
    [Fact]
    public void GetFromYear_NotFoundMatching_ReturnEmpty()
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
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetFromYear(1993);
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }
    
    [Fact]
    public void GetFromGenre_FoundMatching_ReturnMatching()
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
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetFromGenre("testGenre");
        Assert.Equal(3, ((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList().Count);
    }
    
    [Fact]
    public void GetFromGenre_NotFoundMatching_ReturnEmpty()
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
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetFromGenre("action");
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }

    [Fact]
    public void AddMovie_GoodData_Added()
    {
        List<Movie> movies = new List<Movie>
        {
            new("testName",
                "testDescription",
                1992,
                "testGenre"
            ),
        };
        File.WriteAllText( "movies.json", JsonSerializer.Serialize(movies));
        var controller = new MovieController(Utils.LoadMovies());
        var result = controller.GetFromYear(2000);
        Assert.Empty(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
        controller.AddMovie(new Movie("newName", "newDescription", 2000, "newGenre"));
        result = controller.GetFromYear(2000);
        Assert.Single(((IEnumerable<Movie>)((OkObjectResult)result.Result!).Value!).ToList());
    }
}