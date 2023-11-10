using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Extensions;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Repositories;

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
    public async Task<Product> CreateProduct(ProductToAddDto productToAddDto)
    {

        //var item = await (from product in _dbContext.Products
        //                  where product.CategoryId == productToAddDto.CategoryId

        //                  select new Product
        //                  {
        //                      Name = productToAddDto.Name,
        //                      Description = productToAddDto.Description,
        //                      ImageURL = productToAddDto.ImageURL,
        //                      Price = productToAddDto.Price,
        //                      Qty = productToAddDto.Qty,
        //                      CategoryId = productToAddDto.CategoryId,
        //                  }).FirstOrDefaultAsync();

        //if (item != null)
        //{
        //    var result = await _dbContext.Products.AddAsync(item);
        //    await _dbContext.SaveChangesAsync();
        //    return result.Entity;
        //}

        var item = new Product
        {
            Name = productToAddDto.Name,
            Description = productToAddDto.Description,
            ImageURL = productToAddDto.ImageURL,
            Price = productToAddDto.Price,
            Qty = productToAddDto.Qty,
            CategoryId = productToAddDto.CategoryId,
        };

        var result = await _dbContext.Products.AddAsync(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Product> DeleteProduct(int id)
    {
        var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            _dbContext.Products.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        return (item);
    }
}
