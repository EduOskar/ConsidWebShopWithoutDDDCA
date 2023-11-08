using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _dbcontext = applicationDbContext;
    }

    public async Task<User> GetUser(int id)
    {
        var users = await _dbcontext.Users.FindAsync(id);
        return users;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _dbcontext.Users.ToListAsync();
        return users;
    }

}


