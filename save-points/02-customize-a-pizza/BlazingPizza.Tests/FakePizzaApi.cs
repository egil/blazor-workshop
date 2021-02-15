using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;

namespace BlazingPizza
{
    public class FakePizzaApi : IPizzaApi
    {
        private List<Order> orders = new List<Order>();

        internal TaskCompletionSource<IReadOnlyList<PizzaSpecial>> PizzaSpecialTask { get; }
            = new TaskCompletionSource<IReadOnlyList<PizzaSpecial>>();

        internal TaskCompletionSource<IReadOnlyList<Topping>> ToppingTask { get; }
            = new TaskCompletionSource<IReadOnlyList<Topping>>();

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
            => PizzaSpecialTask.Task;

        public Task<IReadOnlyList<Topping>> GetToppingsAsync()
            => ToppingTask.Task;

        public Task<IReadOnlyList<OrderWithStatus>> GetOrdersWithStatusAsync()
        {
            var result = orders
                .OrderByDescending(o => o.CreatedTime)
                .Select(o => OrderWithStatus.FromOrder(o))
                .ToList();

            return Task.FromResult<IReadOnlyList<OrderWithStatus>>(result);
        }

        public Task PlaceOrderAsync(Order order)
        {
            orders.Add(order);
            return Task.CompletedTask;
        }
    }
}
