using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IOrderRepository
{
 
    Task<IEnumerable<Order>> GetOrders(); 
    Task<IEnumerable<OrderItem>> GetOrderItems();
    Task<Order> GetOrder(int id);
    Task<Order> CreateOrder(OrderDto orderDto, List<int> orderItemId);
    Task<Order> DeleteOrder(int id);

}
