using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace CondisWebshop.Web.Components.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
