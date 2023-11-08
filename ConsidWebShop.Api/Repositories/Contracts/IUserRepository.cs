using ConsidWebShop.Api.Entities;
using System.Collections;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int id);
}
