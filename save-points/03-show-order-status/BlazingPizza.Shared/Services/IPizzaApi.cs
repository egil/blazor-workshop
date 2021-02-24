using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public interface IPizzaApi
    {
        Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync();
        Task<IReadOnlyList<Topping>> GetToppingsAsync();
        Task<IReadOnlyList<OrderWithStatus>> GetOrdersWithStatusAsync();
        Task<int> PlaceOrderAsync(Order order);
        IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(int orderId, CancellationToken cancellationToken = default);
    }
}
