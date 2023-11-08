using CondisWebshop.Web.Components.Pages;
using ConsidWebShop.Models.Dtos;

namespace CondisWebshop.Web.Services.Contracts;

public interface IOrderService
{
    Task<IEnumerable<ProductDto>> GetItems();

    Task<ProductDto> GetItem(int id);
}
