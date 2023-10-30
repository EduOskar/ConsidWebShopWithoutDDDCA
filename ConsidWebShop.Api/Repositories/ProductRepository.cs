using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _dbContext.productCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _dbContext.productCategories.SingleOrDefaultAsync(x=>x.Id == id);

            return category;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _dbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            return product;
        }
    }
}
