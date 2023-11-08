using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<OrderItem>> GetOrderItems(int userId);
    Task<Order> GetOrder(int userId);
}
