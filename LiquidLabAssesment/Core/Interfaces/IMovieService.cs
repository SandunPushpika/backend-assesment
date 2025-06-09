using LiquidLabAssesment.DTOs.Models;

namespace LiquidLabAssesment.Core.Interfaces;

public interface IMovieService
{
    Task<List<MovieModel>> FetchAllMovies();
    Task<MovieModel> FetchMovieById(string id);
}