using Microsoft.EntityFrameworkCore;
using MovieMatch.Models;

namespace MovieMatch.Data;

public class MovieMatchDbContext : DbContext
{
    public MovieMatchDbContext(DbContextOptions<MovieMatchDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<UserConnection> UserConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure UserConnection relationships
        modelBuilder.Entity<UserConnection>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.Connections)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserConnection>()
            .HasOne(uc => uc.ConnectedUser)
            .WithMany(u => u.ConnectedBy)
            .HasForeignKey(uc => uc.ConnectedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", Year = 1994, Genre = "Drama", Rating = 9.3, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_SX300.jpg" },
            new Movie { Id = 2, Title = "The Godfather", Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", Year = 1972, Genre = "Crime", Rating = 9.2, ImageUrl = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg" },
            new Movie { Id = 3, Title = "The Dark Knight", Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests.", Year = 2008, Genre = "Action", Rating = 9.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_SX300.jpg" },
            new Movie { Id = 4, Title = "Pulp Fiction", Description = "The lives of two mob hitmen, a boxer, a gangster and his wife intertwine in four tales of violence and redemption.", Year = 1994, Genre = "Crime", Rating = 8.9, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg" },
            new Movie { Id = 5, Title = "Forrest Gump", Description = "The presidencies of Kennedy and Johnson, the Vietnam War, and other historical events unfold from the perspective of an Alabama man.", Year = 1994, Genre = "Drama", Rating = 8.8, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg" },
            new Movie { Id = 6, Title = "Inception", Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea.", Year = 2010, Genre = "Sci-Fi", Rating = 8.8, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SX300.jpg" },
            new Movie { Id = 7, Title = "The Matrix", Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Year = 1999, Genre = "Sci-Fi", Rating = 8.7, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg" },
            new Movie { Id = 8, Title = "Interstellar", Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.", Year = 2014, Genre = "Sci-Fi", Rating = 8.6, ImageUrl = "https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg" },
            new Movie { Id = 9, Title = "Goodfellas", Description = "The story of Henry Hill and his life in the mob, covering his relationship with his wife and his partners in crime.", Year = 1990, Genre = "Crime", Rating = 8.7, ImageUrl = "https://m.media-amazon.com/images/M/MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg" },
            new Movie { Id = 10, Title = "The Silence of the Lambs", Description = "A young FBI cadet must receive the help of an incarcerated cannibal killer to catch another serial killer.", Year = 1991, Genre = "Thriller", Rating = 8.6, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg" }
        );

        // Seed users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Demo User" },
            new User { Id = 2, Name = "Jane Smith" },
            new User { Id = 3, Name = "Bob Johnson" }
        );

        // Seed some ratings for Jane Smith to demonstrate matching
        modelBuilder.Entity<UserRating>().HasData(
            new UserRating { Id = 1, UserId = 2, MovieId = 1, IsLiked = true, RatedAt = DateTime.UtcNow }, // Shawshank Redemption
            new UserRating { Id = 2, UserId = 2, MovieId = 2, IsLiked = true, RatedAt = DateTime.UtcNow }, // The Godfather
            new UserRating { Id = 3, UserId = 2, MovieId = 3, IsLiked = false, RatedAt = DateTime.UtcNow }, // The Dark Knight
            new UserRating { Id = 4, UserId = 2, MovieId = 4, IsLiked = true, RatedAt = DateTime.UtcNow }, // Pulp Fiction
            new UserRating { Id = 5, UserId = 2, MovieId = 6, IsLiked = true, RatedAt = DateTime.UtcNow }  // Inception
        );
    }
}
