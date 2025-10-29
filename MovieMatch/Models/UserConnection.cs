namespace MovieMatch.Models;

public class UserConnection
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ConnectedUserId { get; set; }
    public User ConnectedUser { get; set; } = null!;
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
}
