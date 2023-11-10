using CondisWebshop.Web.Components.Pages;
using ConsidWebShop.Models.Dtos;

namespace CondisWebshop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetItems();
    Task<Products> AddProduct(ProductToAddDto addProduct);

    Task<ProductDto> GetItem(int id);
}
