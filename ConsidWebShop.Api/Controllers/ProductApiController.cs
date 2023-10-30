using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Extensions;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidWebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await _productRepository.GetItems();
                var categories = await _productRepository.GetCategories();
                if (products == null || categories == null)
                {
                    return NotFound();
                }

                var productDto = products.ConvertToDto(categories);

                return Ok(productDto);
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await _productRepository.GetItem(id);
                if (product == null)
                {
                    return NotFound();
                }

                var productCategory = await _productRepository.GetCategory(product.CategoryId);

                var productDto = product.ConvertToDto(productCategory);

                return Ok(productDto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                                "Error retrieving data from the database");
            }

        }
    }
}
