using Microsoft.EntityFrameworkCore;
using MovieMatch.Data;
using MovieMatch.Models;

namespace MovieMatch.Services;

public class UserService
{
    private readonly MovieMatchDbContext _context;

    public UserService(MovieMatchDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Ratings)
            .Include(u => u.Connections)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetConnectedUsersAsync(int userId)
    {
        return await _context.UserConnections
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.ConnectedUser)
            .Select(uc => uc.ConnectedUser)
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<List<User>> GetAvailableUsersToConnectAsync(int userId)
    {
        var connectedUserIds = await _context.UserConnections
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.ConnectedUserId)
            .ToListAsync();

        return await _context.Users
            .Where(u => u.Id != userId && !connectedUserIds.Contains(u.Id))
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task ConnectUsersAsync(int userId, int connectedUserId)
    {
        var existingConnection = await _context.UserConnections
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConnectedUserId == connectedUserId);

        if (existingConnection == null)
        {
            _context.UserConnections.Add(new UserConnection
            {
                UserId = userId,
                ConnectedUserId = connectedUserId,
                ConnectedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task DisconnectUsersAsync(int userId, int connectedUserId)
    {
        var connection = await _context.UserConnections
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConnectedUserId == connectedUserId);

        if (connection != null)
        {
            _context.UserConnections.Remove(connection);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> AreUsersConnectedAsync(int userId, int connectedUserId)
    {
        return await _context.UserConnections
            .AnyAsync(uc => uc.UserId == userId && uc.ConnectedUserId == connectedUserId);
    }
}
