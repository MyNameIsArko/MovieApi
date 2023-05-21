using System.Text.Json;

namespace MovieApi.Helpers;

public class FileUtils : IUtils
{
    private const string Filepath = @"movies.json";

    public void SaveMovies(List<Movie> movies)
    {
        string moviesJson = JsonSerializer.Serialize(movies);
        File.WriteAllText(Filepath, moviesJson);
    }
    public List<Movie> LoadMovies()
    {
        List<Movie> movies = new List<Movie>();
        
        try
        {
            string moviesJson = File.ReadAllText(Filepath);
            movies = JsonSerializer.Deserialize<List<Movie>>(moviesJson) ?? new List<Movie>();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Not found \"movies.json\" file.");
        }

        return movies;
    }
}