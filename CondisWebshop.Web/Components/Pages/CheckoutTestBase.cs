using System.Collections.Generic;
using CondisWebshop.Web.Models;
using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CondisWebshop.Web.Components.Pages;

public class CheckoutTestBase : ComponentBase
{
    //[Inject]
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }
    [Inject]
    public IProductService ProductService { get; set; }
    [Inject]
    public IOrderService OrderService { get; set; }
    protected List<CartItemDto> ShoppingCartCartItems { get; set; }
    protected int TotalQuantity { get; set; }
    protected string PaymentDescription { get; set; }
    protected decimal PaymentAmount { get; set; }

    //Checkoutform for payment
    public CheckoutForm CheckoutForm { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ShoppingCartCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);//Add Users here
            if (ShoppingCartCartItems != null && ShoppingCartCartItems.Count() > 0)
            {
                Guid orderGuid = Guid.NewGuid();

                PaymentAmount = ShoppingCartCartItems.Sum(x => x.TotalPrice);
                TotalQuantity = ShoppingCartCartItems.Sum(x => x.Qty);
                PaymentDescription = $"_{HardCoded.UserId}_{orderGuid}";

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private CartItemDto GetCartItem(int id)
    {
        return ShoppingCartCartItems.FirstOrDefault(x => x.Id == id);
    }

    protected async Task SubmitAsync()
    {
        if (await CartOrderTransfer())
        {
            await Js.InvokeVoidAsync("alert", $"Thank you {CheckoutForm.FirstName} - {CheckoutForm.LastName}, we will deliver to {CheckoutForm.Adress}.");
        }
        else
        {
            await Js.InvokeVoidAsync("alert", $"Failed to process the order. Please try again");
        }

    }
    protected async Task<bool> CartOrderTransfer()
    {
        var customer = HardCoded.UserId;

        if (ShoppingCartCartItems == null || ShoppingCartCartItems.Count == 0)
        {

            return false;

        }

        //var orderItems = ShoppingCartCartItems.Select(cartItem => new OrderItemToAddDto
        //{
        //    Id = cartItem.Id,
        //    ProductId = cartItem.ProductId,
        //    Qty = cartItem.Qty,

        //}).ToList();

        var order = new OrderToAddDto
        {
            UserId = customer,
            PlacementTime = DateTime.Now,
        };

        if (order == null)
        {
            return false;
        }

        await OrderService.PostOrder(order);

        // At this point, order.Id will be populated after saving the order to the database

        foreach (var item in ShoppingCartCartItems)
        {
            var orderItem = new OrderItemToAddDto
            {
                ProductId = item.ProductId,
                OrderId = order.Id, // Now order.Id should be valid
                Qty = item.Qty,
            };

            await OrderService.PostOrderItem(orderItem);
        }

        foreach (var cartItem in ShoppingCartCartItems)
        {
            await ShoppingCartService.DeleteItem(cartItem.ProductId);
        }

        return true;
    }
}



