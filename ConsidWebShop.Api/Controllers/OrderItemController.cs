using ConsidWebShop.Api.Extensions;
using ConsidWebShop.Api.Repositories;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidWebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderItemController : ControllerBase
{
    private readonly OrderItemRepository _orderItemRepository;
    private readonly ProductRepository _productRepository;

    public OrderItemController(OrderItemRepository orderItemRepository, ProductRepository productRepository)
    {
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
    }


    [HttpGet("{userId:int}/GetOrderItems")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(int userId)
    {
        try
        {
            var orderItems = await _orderItemRepository.GetOrderItem(userId);
            var products = await _productRepository.GetItem(userId);
            if (orderItems == null || products == null)
            {
                return NotFound();
            }
            var orderItemDto = orderItems.ConvertToDto(products);

            return Ok(orderItemDto);

        }
        catch (Exception)
        {

            throw;

        }
    }
    [HttpGet("{userId:int}/GetOrderItem")]
    public async Task<ActionResult<OrderItemDto>> GetItem(int userId)
    {
        try
        {
            var orderItem = await _orderItemRepository.GetOrderItem(userId);
            if (orderItem == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetItem(orderItem.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var orderItemDto = orderItem.ConvertToDto(product);
            return Ok(orderItem);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost]
    [Route("AddOrderItem")]
    public async Task<ActionResult<OrderItemDto>> AddOrderItem([FromBody] OrderItemToAddDto orderItemToAddDto)
    {
        try
        {
            var newOrderItem = await _orderItemRepository.AddOrderItem(orderItemToAddDto);
            if (newOrderItem == null)
            {
                return NoContent();
            }
            var product = await _productRepository.GetItem(newOrderItem.ProductId);
            if (product == null)
            {
                throw new Exception($"Something went wrong when attempting to retrieve product (productId:({newOrderItem.ProductId}))");
            }
            var newOrderItemDto = newOrderItem.ConvertToDto(product);

            return CreatedAtAction(nameof(GetOrderItems), new { id = newOrderItemDto.Id }, newOrderItemDto);
            throw new NotImplementedException();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
