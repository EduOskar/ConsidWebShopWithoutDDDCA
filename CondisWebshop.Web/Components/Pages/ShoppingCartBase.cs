using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace CondisWebshop.Web.Components.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId); //Add in Users here 
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
    }
}
