using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;

namespace BlazingPizza
{
    public sealed class LoadingForeverPizzaApi : IPizzaApi, IDisposable
    {
        private readonly TaskCompletionSource loadTask = new TaskCompletionSource();

        public void Dispose()
        {
            loadTask.SetResult();
        }

        public async Task<IReadOnlyList<OrderWithStatus>> GetOrdersWithStatusAsync()
        {
            await loadTask.Task;
            return Array.Empty<OrderWithStatus>();
        }

        public async IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(int orderId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await loadTask.Task;
            yield break;
        }

        public async Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
        {
            await loadTask.Task;
            return Array.Empty<PizzaSpecial>();
        }

        public async Task<IReadOnlyList<Topping>> GetToppingsAsync()
        {
            await loadTask.Task;
            return Array.Empty<Topping>();
        }

        public async Task<int> PlaceOrderAsync(Order order)
        {
            await loadTask.Task;
            return 0;
        }
    }
}
