using Microsoft.EntityFrameworkCore;
using MovieMatch.Data;
using MovieMatch.Models;

namespace MovieMatch.Services;

public class MatchingService
{
    private readonly MovieMatchDbContext _context;

    public MatchingService(MovieMatchDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetCommonLikedMoviesAsync(int userId, int connectedUserId)
    {
        var userLikedMovieIds = await _context.UserRatings
            .Where(ur => ur.UserId == userId && ur.IsLiked)
            .Select(ur => ur.MovieId)
            .ToListAsync();

        var connectedUserLikedMovieIds = await _context.UserRatings
            .Where(ur => ur.UserId == connectedUserId && ur.IsLiked)
            .Select(ur => ur.MovieId)
            .ToListAsync();

        var commonMovieIds = userLikedMovieIds.Intersect(connectedUserLikedMovieIds).ToList();

        return await _context.Movies
            .Where(m => commonMovieIds.Contains(m.Id))
            .OrderByDescending(m => m.Rating)
            .ToListAsync();
    }

    public async Task<Dictionary<int, List<Movie>>> GetCommonLikedMoviesForAllConnectionsAsync(int userId)
    {
        var connections = await _context.UserConnections
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.ConnectedUser)
            .ToListAsync();

        var result = new Dictionary<int, List<Movie>>();

        foreach (var connection in connections)
        {
            var commonMovies = await GetCommonLikedMoviesAsync(userId, connection.ConnectedUserId);
            result[connection.ConnectedUserId] = commonMovies;
        }

        return result;
    }
}
