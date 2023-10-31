using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace CondisWebshop.Web.Components.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

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

        protected async Task DeleteCartItem_click(int id)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.DeleteItem(id);

                RemoveCartItem(id);
            }
            catch (Exception)
            {
                //logg exception
                throw;
            }
            
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDo = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDo);

        }
    }
}
