﻿using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CondisWebshop.Web.Components.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js {  get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; } = new List<CartItemDto>();
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; } = string.Empty;
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId); //Add in Users here 
                CartChanged();
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
                CartChanged();

                await MakeUpdateQtyButtonVisible(id, false);
            }
            catch (Exception)
            {
                //logg exception
                throw;
            }

        }

        protected async Task UpdateQty_input(int id)
        {
            await MakeUpdateQtyButtonVisible(id, true);
        }
        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };

                    var returnredUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);

                    UpdateItemTotalPrice(returnredUpdateItemDto);

                    CartChanged();
                    await MakeUpdateQtyButtonVisible(id, false);

                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(x => x.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {

            TotalPrice = ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");

        }
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(x => x.Qty);
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
        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    
    }
}
