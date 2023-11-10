using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetOrderItems(int userId);
    Task<OrderItem> AddOrderItem(OrderItemToAddDto orderItemToAddDto);
    Task<OrderItem> GetOrderItem(int userId);
}
