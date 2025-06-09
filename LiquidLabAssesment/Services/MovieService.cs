using LiquidLabAssesment.Core.Helpers;
using LiquidLabAssesment.Core.Interfaces;
using LiquidLabAssesment.DTOs.Models;
using LiquidLabAssesment.DTOs.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LiquidLabAssesment.Services;

public class MovieService(IDbService dbService, IOptions<AppConfig> options, HttpHelper helper) : IMovieService
{

    public async Task<List<MovieModel>> FetchAllMovies()
    {
        var movies = await dbService.FetchAllAsync<MovieModel>(MovieQueries.GET_MOVIES);
        if (movies.IsNullOrEmpty())
        {
            await FetchAllMoviesFromApi(movies);
            await InsertMovies(movies);
        }
        
        return movies;
    }

    public async Task<MovieModel> FetchMovieById(string id)
    {
        var movie = await dbService.FetchFirst<MovieModel>(MovieQueries.GET_BY_ID, id);
        if (movie == null)
        {
            var movieModel = await FetchMovieFromApi(id.ToLower());
            if (movieModel != null)
            {
                await InsertMovies([movieModel]);
            }
            
            return movieModel;
        }
        
        return movie;
    }
    
    private async Task FetchAllMoviesFromApi(List<MovieModel> movies)
    {
        string apiUrl = options.Value.DemoApiUrl + "/films";
        var res = await helper.GetAsync<List<MovieResponse>>(apiUrl);
        
        if (res.IsNullOrEmpty())
            return;
        
        res.ForEach(response =>
        {
            var movie = new MovieModel()
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                Director = response.Director,
                Producer = response.Producer,
                OriginalTitle = response.OriginalTitle,
                ReleaseDate = response.ReleaseDate,
                OriginalTitleRomanised = response.OriginalTitleRomanised,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            movies.Add(movie);
        });
    }

    private async Task<MovieModel> FetchMovieFromApi(string id)
    {
        string apiUrl = $"{options.Value.DemoApiUrl}/films/{id}";
        var response = await helper.GetAsync<MovieResponse>(apiUrl);
        
        if(response == null)
            return null;
        
        return new MovieModel()
        {
            Id = response.Id,
            Title = response.Title,
            Description = response.Description,
            Director = response.Director,
            Producer = response.Producer,
            OriginalTitle = response.OriginalTitle,
            ReleaseDate = response.ReleaseDate,
            OriginalTitleRomanised = response.OriginalTitleRomanised,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }

    private async Task InsertMovies(List<MovieModel> movies)
    {
        foreach (var movie in movies)
        {
            await dbService.ExecuteAsync(MovieQueries.INSERT_MOVIE,
                movie.Id.ToString(),
                movie.Title,
                movie.OriginalTitle,
                movie.OriginalTitleRomanised,
                movie.Description,
                movie.Director,
                movie.Producer,
                movie.ReleaseDate,
                movie.CreatedAt,
                movie.UpdatedAt
            );
        }
    }
    
}