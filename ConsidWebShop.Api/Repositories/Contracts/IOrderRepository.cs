using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IOrderRepository
{
 
    Task<IEnumerable<Order>> GetOrders(); 
    Task<IEnumerable<OrderItem>> GetOrderItems();
    Task<OrderItem> CreateOrderItem(OrderItemToAddDto orderItemToAddDto);
    Task<Order> GetOrder(int id);
    Task<Order> CreateOrder(OrderToAddDto orderDto);
    Task<Order> DeleteOrder(int id);

}
