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
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        return Ok(order);
    }
    [HttpGet("/Order/{id:int}")]
    public IActionResult GetOrder(int id)
    {
        return Ok(id);
    }
    [HttpPut]
    public IActionResult ChangeOrder(Order order, int id) 
    { 
        return Ok(order);
    }
    [HttpDelete("/Order/{id:int}")]
    public IActionResult DeleteOrder(int id)
    {
        return Ok(id);
    }
}
