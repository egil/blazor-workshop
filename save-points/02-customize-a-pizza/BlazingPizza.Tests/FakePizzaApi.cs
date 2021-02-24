using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;

namespace BlazingPizza
{
    public class FakePizzaApi : IPizzaApi
    {
        internal List<Order> Orders { get; } = new List<Order>();

        internal TaskCompletionSource<IReadOnlyList<OrderWithStatus>> OrderWithStatusTask { get; }
            = new TaskCompletionSource<IReadOnlyList<OrderWithStatus>>();

        internal TaskCompletionSource<OrderWithStatus> OrderUpdateTask { get; private set; }
            = new TaskCompletionSource<OrderWithStatus>();

        internal TaskCompletionSource<IReadOnlyList<PizzaSpecial>> PizzaSpecialTask { get; }
            = new TaskCompletionSource<IReadOnlyList<PizzaSpecial>>();

        internal TaskCompletionSource<IReadOnlyList<Topping>> ToppingTask { get; }
            = new TaskCompletionSource<IReadOnlyList<Topping>>();

        internal void CompleteOrderWithStatusRequest()
        {
            var result = Orders
                .OrderByDescending(o => o.CreatedTime)
                .Select(o => OrderWithStatus.FromOrder(o))
                .ToList();

            OrderWithStatusTask.SetResult(result);
        }

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
            => PizzaSpecialTask.Task;

        public Task<IReadOnlyList<Topping>> GetToppingsAsync()
            => ToppingTask.Task;

        public Task<IReadOnlyList<OrderWithStatus>> GetOrdersWithStatusAsync()
            => OrderWithStatusTask.Task;

        public Task PlaceOrderAsync(Order order)
        {
            Orders.Add(order);
            return Task.CompletedTask;
        }

        public async IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(int orderId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await OrderUpdateTask.Task;
                OrderUpdateTask = new TaskCompletionSource<OrderWithStatus>();

                if (result.Order.OrderId == orderId)
                    yield return result;
            }
        }
    }
}
