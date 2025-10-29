namespace MovieMatch.Models;

public class UserRating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public bool IsLiked { get; set; }
    public DateTime RatedAt { get; set; } = DateTime.UtcNow;
}
