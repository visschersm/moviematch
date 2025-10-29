using Microsoft.EntityFrameworkCore;
using MovieMatch.Data;
using MovieMatch.Models;

namespace MovieMatch.Services;

public class MovieService
{
    private readonly MovieMatchDbContext _context;

    public MovieService(MovieMatchDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .OrderByDescending(m => m.Rating)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.UserRatings)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Movie>> GetUnratedMoviesForUserAsync(int userId)
    {
        var ratedMovieIds = await _context.UserRatings
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.MovieId)
            .ToListAsync();

        return await _context.Movies
            .Where(m => !ratedMovieIds.Contains(m.Id))
            .OrderByDescending(m => m.Rating)
            .ToListAsync();
    }

    public async Task RateMovieAsync(int userId, int movieId, bool isLiked)
    {
        var existingRating = await _context.UserRatings
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.MovieId == movieId);

        if (existingRating != null)
        {
            existingRating.IsLiked = isLiked;
            existingRating.RatedAt = DateTime.UtcNow;
        }
        else
        {
            _context.UserRatings.Add(new UserRating
            {
                UserId = userId,
                MovieId = movieId,
                IsLiked = isLiked,
                RatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetLikedMoviesByUserAsync(int userId)
    {
        return await _context.UserRatings
            .Where(ur => ur.UserId == userId && ur.IsLiked)
            .Include(ur => ur.Movie)
            .Select(ur => ur.Movie)
            .OrderBy(m => m.Title)
            .ToListAsync();
    }

    public async Task<bool> HasUserRatedMovieAsync(int userId, int movieId)
    {
        return await _context.UserRatings
            .AnyAsync(ur => ur.UserId == userId && ur.MovieId == movieId);
    }

    public async Task<bool?> GetUserRatingAsync(int userId, int movieId)
    {
        var rating = await _context.UserRatings
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.MovieId == movieId);
        
        return rating?.IsLiked;
    }
}
