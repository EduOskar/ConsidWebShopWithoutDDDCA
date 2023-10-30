using ConsidWebShop.Models.Dtos;

namespace CondisWebshop.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();

        Task<ProductDto> GetItem(int id);
    }
}
