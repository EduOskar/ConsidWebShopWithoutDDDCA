using CondisWebshop.Web.Components.Pages;
using ConsidWebShop.Models.Dtos;

namespace CondisWebshop.Web.Services.Contracts;

public interface IOrderService
{
    Task<List<OrderItemDto>> GetOrderItems(int userId);
    Task<OrderDto> GetOrder(int userId);
}
