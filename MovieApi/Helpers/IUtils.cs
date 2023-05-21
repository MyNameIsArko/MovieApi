namespace MovieApi.Helpers;

public interface IUtils
{
    public void SaveMovies(List<Movie> movies);

    public List<Movie> LoadMovies();
}