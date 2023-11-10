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
    IOrderService OrderService { get; set; }
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
        await CartOrderTransfer();
        await Js.InvokeVoidAsync("alert", $"Thank you {CheckoutForm.FirstName} - {CheckoutForm.LastName}, we will deliver to {CheckoutForm.Adress}.");
        
    }
    public async Task<bool> CartOrderTransfer()
    {
        decimal Check;
        var customer =  HardCoded.UserId;

        if (ShoppingCartCartItems == null)
        {
            return false;
        }
        if (HardCoded.Money < PaymentAmount)
        {
            return false;
        }
        foreach (var item in ShoppingCartCartItems)
        {
            item.Qty--;
            var order = new OrderDto
            {
                CustomerId = customer,
                OrderPlacementTime = DateTime.Now,
                OrderItemsId = ShoppingCartCartItems.ToList().First().Id,

            };
            await OrderService.AddOrder(order);
            foreach (var newOrderItem in ShoppingCartCartItems)
            {
                var orderItem = new OrderItemToAddDto
                {
                    ProductId = newOrderItem.ProductId,
                    OrderId = order.Id,
                    Qty = item.Qty++,
                };
                await OrderService.AddOrderItem(orderItem);
            }            
        }
        return default;
    }
}



