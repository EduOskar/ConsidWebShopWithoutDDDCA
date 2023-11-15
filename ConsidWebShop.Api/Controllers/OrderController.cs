using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ConsidWebShop.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using ConsidWebShop.Api.Repositories;

namespace ConsidWebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        try
        {
            var orders = await _orderRepository.GetOrders();
            var orderItems = await _orderRepository.GetOrderItems();
            if (orders == null || orderItems == null)
            {
                return NotFound();
            }
            var orderDto = orders.ConvertToDto(orderItems);
            return Ok(orderDto);
        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderItems = await _orderRepository.GetOrderItems();

            var orderDto = order.ConvertToDto(orderItems);

            return Ok(orderDto);
        }
        catch (Exception)
        {

            throw;
        }
        
    }
    [HttpPost]
    [Route("PostOrderItem")]
    public async Task<ActionResult<OrderItemDto>> PostOrderItem([FromBody] OrderItemToAddDto orderItemToAddDto)
    {
        try
        {
            var orderItem = await _orderRepository.CreateOrderItem(orderItemToAddDto);
            if (orderItem == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(PostOrderItem), orderItemToAddDto);
        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<OrderDto>> PostOrder([FromBody] OrderToAddDto orderToAddDto)
    {
        var order = await _orderRepository.CreateOrder(orderToAddDto);
        if (order == null)
        {
            return NoContent();
        }
        var orderItems = await _orderRepository.GetOrderItems();
        var newOrderDto = order.ConvertToDto(orderItems);

        return CreatedAtAction(nameof(PostOrder), newOrderDto);
    }

 
    [HttpPut]
    public IActionResult PutOrder(OrderDto order, int id) 
    { 
        return Ok(order);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OrderDto>> DeleteOrder(int id)
    {
        var order = await _orderRepository.DeleteOrder(id);
        if (order == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
