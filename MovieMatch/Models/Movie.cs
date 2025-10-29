namespace MovieMatch.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int Year { get; set; }
    public string? Genre { get; set; }
    public double Rating { get; set; }
    public ICollection<UserRating> UserRatings { get; set; } = new List<UserRating>();
}
