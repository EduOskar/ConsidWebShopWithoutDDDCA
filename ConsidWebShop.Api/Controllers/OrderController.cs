using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ConsidWebShop.Api.Extensions;

namespace ConsidWebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepositorycs;

    public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepositorycs)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepositorycs = userRepositorycs;
    }
    [HttpGet("{id:int}/GetOrderItem")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(int userId)
    {
        try
        {
            var orderItems = await _orderRepository.GetOrderItems(userId);
            var products = await _productRepository.GetItems();
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
    [HttpGet("{id:int}/GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(int userId)
    {
        try
        {
            var order = await _orderRepository.GetOrder(userId);
            
            if (order == null)
            {
                return NotFound();
            }
            //var user = await _userRepositorycs.GetUser(userId);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            var orderDto = order.ConvertToDto;
            return Ok(orderDto);
        }
        catch (Exception)
        {

            throw;
        }
    }
    
}
