using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public class PizzaApi : IPizzaApi
    {
        private readonly HttpClient httpClient;

        public PizzaApi(HttpClient httpClient)
            => this.httpClient = httpClient;

        public async Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
            => await httpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");

        public async Task<IReadOnlyList<Topping>> GetToppingsAsync()
            => await httpClient.GetFromJsonAsync<List<Topping>>("toppings");

        public async Task<IReadOnlyList<OrderWithStatus>> GetOrdersAsync()
            => await httpClient.GetFromJsonAsync<List<OrderWithStatus>>("orders");

        public async Task<OrderWithStatus> GetOrderAsync(int orderId)
            => await httpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}");

        public async Task<int> PlaceOrderAsync(Order order)
        {
            var response = await httpClient.PostAsJsonAsync("orders", order);
            response.EnsureSuccessStatusCode();
            var orderId = await response.Content.ReadFromJsonAsync<int>();
            return orderId;
        }

        public async IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(
            int orderId,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var orderWithStatus = await httpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}", cancellationToken);
                yield return orderWithStatus;
                await Task.Delay(4000, cancellationToken);
            }
        }
    }
}
