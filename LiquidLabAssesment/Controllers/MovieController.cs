using LiquidLabAssesment.Core.Interfaces;
using LiquidLabAssesment.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LiquidLabAssesment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var res = await _movieService.FetchAllMovies();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(string id)
    {
        var movie = await _movieService.FetchMovieById(id);
        if (movie == null)
        {
            return NotFound(new CustomHttpResponse()
            {
                StatusCode = 404,
                Message = "Movie is not found"
            });
        }
        
        return Ok(movie);
    }
}