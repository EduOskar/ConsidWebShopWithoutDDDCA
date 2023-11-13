﻿using ConsidWebShop.Api.Data;
using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Extensions;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidWebShop.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ProductDto>>> Items()
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
    public async Task<ActionResult<ProductDto>> Item(int id)
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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Product>> PostProduct([FromBody] ProductToAddDto productToAddDto)
    {
        try
        {
            var newProduct = await _productRepository.CreateProduct(productToAddDto);
            if (newProduct == null)
            {
                return NoContent();
            }

            var productDto = newProduct.CategoryId;

            return CreatedAtAction(nameof(PostProduct), productDto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                                            "Error retrieving data from the database");
        }
    }
    [HttpDelete]
    public async Task<ActionResult<ProductDto>> DeleteItem(int id)
    {
        try
        {
            var product = await _productRepository.DeleteProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                                            "Error retrieving data from the database");
        }
    }
}
