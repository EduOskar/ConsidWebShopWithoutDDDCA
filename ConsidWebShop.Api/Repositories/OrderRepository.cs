using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbcontext)
    {
        _dbContext = dbcontext;
    }
    public async Task<Order> GetOrder(int userId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.UserId == userId);
        return order;
                      
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItems(int userId)
    {

        return await (from order in _dbContext.Orders
                      join orderItem in _dbContext.OrderItems
                      on order.Id equals orderItem.OrderId
                      where order.UserId == userId
                      select new OrderItem
                      {
                          Id = orderItem.Id,
                          OrderId = orderItem.OrderId,
                          ProductId = orderItem.ProductId
                          
                      }).ToListAsync();
    }
}
