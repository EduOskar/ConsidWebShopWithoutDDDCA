using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<Order> AddOrder(OrderDto orderDto)
    {
        throw new NotImplementedException();
    }
    public async Task<Order> GetOrder(int userId)
    {

        throw new NotImplementedException();

    }

}
