namespace MovieMatch.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<UserRating> Ratings { get; set; } = new List<UserRating>();
    public ICollection<UserConnection> Connections { get; set; } = new List<UserConnection>();
    public ICollection<UserConnection> ConnectedBy { get; set; } = new List<UserConnection>();
}
