using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts;

public interface IOrderRepository
{
 
    Task<Order> GetOrder(int userId);
    Task<Order> AddOrder(OrderDto orderDto);
}
