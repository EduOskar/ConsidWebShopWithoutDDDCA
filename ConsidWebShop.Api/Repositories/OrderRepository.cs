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
        //var cartItems = await _dbContext.CartItems.ToListAsync();

        var orderItems = await _dbContext.OrderItems
            .Include(p => p.Product)
            .ToListAsync();
        return orderItems;
    }
    public async Task<OrderItem> CreateOrderItem(OrderItemToAddDto orderItemToAddDto)
    {
        var orderItem = new OrderItem
        {
            OrderId = orderItemToAddDto.OrderId,
            ProductId = orderItemToAddDto.ProductId,
            Qty = orderItemToAddDto.Qty,
        };

        var result = await _dbContext.OrderItems.AddAsync(orderItem);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
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
    public async Task<Order> CreateOrder(OrderToAddDto orderDto)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in orderItems)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(item);

            if (orderItems != null)
            {
                orderItems.Add(orderItem);
            }
            else
            {
                throw new InvalidOperationException($"Order item with ID not found.");
            }

        }

        var order = new Order
        {
            UserId = orderDto.UserId,
            PlacementTime = orderDto.PlacementTime,
            OrderItemId = orderDto.OrderItemId,
            OrderItems = orderItems
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
