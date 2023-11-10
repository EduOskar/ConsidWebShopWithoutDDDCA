using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderItemRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    public async Task<OrderItem> GetOrderItem(int userId)
    {
        return await (from order in _dbContext.Orders
                      join orderItem in _dbContext.OrderItems
                      on order.Id equals orderItem.OrderId
                      where orderItem.Id == userId
                      select new OrderItem
                      {
                          Id = orderItem.Id,
                          ProductId = orderItem.ProductId,
                          OrderId = orderItem.OrderId,
                          Qty = orderItem.Qty
                      }).SingleOrDefaultAsync();
    }
    private async Task<bool> OrderItemExist(int orderId, int productId)
    {
        return await _dbContext.OrderItems.AnyAsync(c => c.OrderId == orderId &&
                                                    c.ProductId == productId);
    }
    public async Task<OrderItem> AddOrderItem(OrderItemToAddDto orderItemToAddDto)
    {
        if (await OrderItemExist(orderItemToAddDto.OrderId, orderItemToAddDto.ProductId) == false)
        {
            var item = await (from product in _dbContext.Products
                              where product.Id == orderItemToAddDto.ProductId
                              select new OrderItem
                              {
                                  OrderId = orderItemToAddDto.OrderId,
                                  ProductId = product.Id,
                                  Qty = orderItemToAddDto.Qty
                              }).SingleOrDefaultAsync();
            if (item != null)
            {
                var result = await _dbContext.OrderItems.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;

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
                          ProductId = orderItem.ProductId,
                      }).ToListAsync();
    }
}
