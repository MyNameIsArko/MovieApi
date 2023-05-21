using MovieApi.Helpers;
using Xunit;

namespace MovieApi.Tests;

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
        new FileUtils().SaveMovies(movies);
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
        new FileUtils().SaveMovies(expected);
        var result = new FileUtils().LoadMovies();
        Assert.Equal(expected, result);
    }
}