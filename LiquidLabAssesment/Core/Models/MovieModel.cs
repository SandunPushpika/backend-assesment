using LiquidLabAssesment.DTOs.Responses;

namespace LiquidLabAssesment.DTOs.Models;

public class MovieModel : MovieResponse
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}