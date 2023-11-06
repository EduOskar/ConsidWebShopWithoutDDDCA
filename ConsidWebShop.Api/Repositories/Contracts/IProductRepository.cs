using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id); 
        Task<Product> AddProduct(ProductToAddDto productToAddDto);
        Task<Product> DeleteProduct(int Id);
    }
}
