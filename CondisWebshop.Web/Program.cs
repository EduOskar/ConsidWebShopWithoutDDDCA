using CondisWebshop.Web.Components;
using CondisWebshop.Web.Services;
using CondisWebshop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7012/") });
builder.Services.AddHttpClient<IProductService, ProductService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7012/"));
builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7012/"));

await builder.Build().RunAsync();
