using ConsidWebShop.Api.Entities;
using System.Collections;

namespace ConsidWebShop.Api.Repositories.Contracts
{
    public interface IUserRepositorycs
    {
        IEnumerable<Task<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}
