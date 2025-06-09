namespace LiquidLabAssesment.Core.Helpers;

public class MovieQueries
{
    public static readonly string GET_MOVIES = "SELECT * FROM Movies";
    
    public static readonly string GET_BY_ID = "SELECT * FROM Movies WHERE Id = @p0";

    public static readonly string INSERT_MOVIE = "INSERT INTO Movies (Id, Title, OriginalTitle, OriginalTitleRomanised,Description, Director, Producer, ReleaseDate, CreatedAt, UpdatedAt) VALUES ( @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";
}