using CondisWebshop.Web.Components.Pages;
using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using System.Net.Http.Json;

namespace CondisWebshop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            ConfigureClient();
        }
        private void ConfigureClient()
        {
            _httpClient.BaseAddress =
                new Uri("https://localhost:7012/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ProductApi/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ProductApi");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
   
        public async Task<Products> AddProduct(ProductToAddDto addProduct)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ProductApi", addProduct);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<Products>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
