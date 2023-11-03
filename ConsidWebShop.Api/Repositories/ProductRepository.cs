using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var categories = await _dbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _dbContext.ProductCategories.SingleOrDefaultAsync(x => x.Id == id);

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
        private async Task<bool> ProductExist(int ProductId)
        {
            return await _dbContext.Products.AnyAsync(x => x.Id == ProductId);
        }
        public async Task<Product> AddItem(ProductDto productDto)
        {
            if (await ProductExist(productDto.Id) == false)
            {
                var item = await (from product in _dbContext.Products
                            where product.Id == productDto.Id
                            select new Product
                            {
                                Id = productDto.Id,
                                Name = productDto.Name,
                                Description = productDto.Description,
                                ImageURL = productDto.ImageURL,
                                Price = productDto.Price,
                                Qty = productDto.Qty,
                                CategoryId = productDto.CategoryId,

                            }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await _dbContext.Products.AddAsync(item);
                    _dbContext.SaveChanges();
                    return result.Entity;
                }
            }
            return null;
        }

        public Task<Product> RemoveProduct(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
