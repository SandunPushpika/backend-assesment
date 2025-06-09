namespace LiquidLabAssesment.DTOs.Responses;

public class MovieResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string OriginalTitleRomanised { get; set; }
    public string Description { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public string ReleaseDate { get; set; } 
}