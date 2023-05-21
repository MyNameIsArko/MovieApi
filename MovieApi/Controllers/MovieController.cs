using Microsoft.AspNetCore.Mvc;
using MovieApi.Helpers;

namespace MovieApi.Controllers;

[ApiController]
[Route("/")]
public class MovieController : ControllerBase
{
    private readonly List<Movie> _movies;
    
    public MovieController(List<Movie> movies)
    {
        _movies = movies;
    }

    [HttpGet]
    public ActionResult<Movie> GetMovie()
    {
        return _movies.Count > 0 ? Ok(_movies[^1]) : BadRequest();
    }
    
    [HttpGet("{year:int}")]
    public ActionResult<IEnumerable<Movie>> GetFromYear(int year)
    {
        return Ok(_movies.Where(movie => movie.ReleaseYear == year));
    }

    [HttpGet("{genre}")]
    public ActionResult<IEnumerable<Movie>> GetFromGenre(string genre)
    {
        return Ok(_movies.Where(movie => movie.Genre == genre));
    }

    [HttpPost]
    public ActionResult AddMovie(Movie movie)
    {
        _movies.Add(movie);
        new FileUtils().SaveMovies(_movies);
        return Ok();
    }
    

}