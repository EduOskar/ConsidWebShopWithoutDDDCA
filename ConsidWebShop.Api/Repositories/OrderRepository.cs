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
    public async Task<IEnumerable<OrderItem>> GetOrderItems()
    {
        var orderItems = await _dbContext.OrderItems
            .Include(p => p.Product)
            .ToListAsync();
        return orderItems;
    }
    public async Task<IEnumerable<Order>> GetOrders()
    {
        var order = await _dbContext.Orders.ToListAsync();
        return order;
    }
    public async Task<Order> GetOrder(int id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        return order;
    }
    public async Task<Order> CreateOrder(OrderDto orderDto, List<int> orderItemId)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in orderItemId)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(item);

            if (orderItems != null)
            {
                orderItems.Add(orderItem);
            }
            else
            {
                throw new InvalidOperationException($"Order item with ID {orderItemId} not found.");
            }

        }

        var order = new Order
        {
            UserId = orderDto.UserId,
            PlacementTime = orderDto.OrderPlacementTime,
            OrderItemId = orderDto.OrderItemsId,
            OrderItems = orderDto.OrderItems.Select(orderItemDto => new OrderItem
            {
                Id = orderItemDto.Id,
                ProductId = orderItemDto.ProductId,
                OrderId = orderItemDto.OrderId,
                
            }).ToList()
            
        };
        var result = await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return result.Entity;

    }

    public async Task<Order> DeleteOrder(int id)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
        return (order);
    }
}
