using MovieApi.Helpers;
using Xunit;

namespace MovieApi.Tests;

[Collection("MovieApi")]
public class TestUtils
{
    [Fact]
    public void SaveMovies_JsonSaved_True()
    {
        try
        {
            File.Delete("movies.json");
        }
        catch (Exception)
        {
            // ignored
        }
        List<Movie> movies = new List<Movie>
        {
            new("test1", "test2", 2000, "test3")
        };
        Utils.SaveMovies(movies);
        Assert.True(File.Exists("movies.json"));
        string text = File.ReadAllText("movies.json");
        string expected = """[{"Name":"test1","Description":"test2","ReleaseYear":2000,"Genre":"test3"}]""";
        Assert.Equal(expected, text);
    }
    
    [Fact]
    public void LoadMovies_ListLoaded_True()
    {
        List<Movie> expected = new List<Movie>
        {
            new("test1", "test2", 2000, "test3"),
            new("test4", "test5", 2012, "test6")
        };
        Utils.SaveMovies(expected);
        var result = Utils.LoadMovies();
        Assert.Equal(expected, result);
    }
}