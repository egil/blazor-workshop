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
        public const string AuthenticatedClientName = nameof(PizzaApi) + nameof(AuthenticatedClientName);
        public const string UnauthenticatedClientName = nameof(PizzaApi) + nameof(UnauthenticatedClientName);

        private readonly HttpClient authedHttpClient;
        private readonly HttpClient unauthedHttpClient;

        public PizzaApi(IHttpClientFactory httpClientFactory)
        {
            authedHttpClient = httpClientFactory.CreateClient(AuthenticatedClientName);
            unauthedHttpClient = httpClientFactory.CreateClient(UnauthenticatedClientName);
        }

        public async Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
            => await unauthedHttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");

        public async Task<IReadOnlyList<Topping>> GetToppingsAsync()
            => await unauthedHttpClient.GetFromJsonAsync<List<Topping>>("toppings");

        public async Task<IReadOnlyList<OrderWithStatus>> GetOrdersAsync()
            => await authedHttpClient.GetFromJsonAsync<List<OrderWithStatus>>("orders");

        public async Task<OrderWithStatus> GetOrderAsync(int orderId)
            => await authedHttpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}");

        public async Task<int> PlaceOrderAsync(Order order)
        {
            var response = await authedHttpClient.PostAsJsonAsync("orders", order);
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
                var orderWithStatus = await authedHttpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}", cancellationToken);
                yield return orderWithStatus;
                await Task.Delay(4000, cancellationToken);
            }
        }
    }
}
