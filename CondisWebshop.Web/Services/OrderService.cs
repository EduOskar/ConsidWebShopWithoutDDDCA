using CondisWebshop.Web.Services.Contracts;
using ConsidWebShop.Models.Dtos;
using System.Net.Http.Json;

namespace CondisWebshop.Web.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
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

    public async Task<List<OrderItemDto>> GetOrderItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Order/{userId}/GetOrderItem");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<OrderItemDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<OrderItemDto>>();

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<OrderDto> GetOrder(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Order/{userId}/GetOrder");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default;
                }
                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
