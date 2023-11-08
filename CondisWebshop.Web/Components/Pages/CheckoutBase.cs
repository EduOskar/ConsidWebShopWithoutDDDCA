using System.Collections.Generic;
using CondisWebshop.Web.Models;
using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CondisWebshop.Web.Components.Pages;

public class CheckoutBase : ComponentBase
{
    //[Inject]
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }
    [Inject]
    public IProductService ProductService { get; set; }
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
        await Js.InvokeVoidAsync("alert", $"Thank you {CheckoutForm.FirstName} - {CheckoutForm.LastName}, we will deliver to {CheckoutForm.Adress}.");

        if (HardCoded.Money > PaymentAmount)
        {
            ShoppingCartCartItems.Clear();
        }
        

    }


}



