using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace CondisWebshop.Web.Components.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();

                var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                var totalQty = shoppingCartItems.Sum(i => i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
            
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedPRoductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected static string GetCategoryName(IGrouping<int, ProductDto> groupedProductDto)
        {
            return groupedProductDto.FirstOrDefault(pg => pg.CategoryId == groupedProductDto.Key).CategoryName;
        }
    }
}
