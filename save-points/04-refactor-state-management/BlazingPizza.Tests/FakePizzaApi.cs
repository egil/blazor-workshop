using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BlazingPizza.Client.Services;

namespace BlazingPizza
{
    public class FakePizzaApi : IPizzaApi
    {
        private TaskCompletionSource<OrderWithStatus> orderStatusUpdated = new();
        private int orderIdSequence = 0;
        private readonly List<Topping> toppings;
        private readonly List<PizzaSpecial> pizzaSpecials;
        private readonly List<Order> orders = new List<Order>();
        private readonly List<OrderWithStatus> orderWithStatuses = new List<OrderWithStatus>();

        public FakePizzaApi(IEnumerable<Topping>? toppings = null,
                            IEnumerable<PizzaSpecial>? pizzaSpecials = null)
        {
            var fixture = new Fixture();
            this.toppings = toppings?.ToList() 
                ?? fixture.CreateMany<Topping>(10).ToList<Topping>();
            this.pizzaSpecials = pizzaSpecials?.ToList() 
                ?? fixture.CreateMany<PizzaSpecial>(20).ToList();
        }

        public Task<IReadOnlyList<OrderWithStatus>> GetOrdersWithStatusAsync()
        {
            return Task.FromResult<IReadOnlyList<OrderWithStatus>>(orderWithStatuses);
        }

        public async IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(int orderId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var order = orderWithStatuses.SingleOrDefault(x => x.Order.OrderId == orderId);

            if (order is null)
            {
                throw new HttpRequestException("Invalid order id");
            }

            yield return order;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updatedOrder = await orderStatusUpdated.Task;
                
                if(updatedOrder.Order.OrderId == orderId)
                {
                    yield return updatedOrder;
                }                
            }
        }

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
        {
            return Task.FromResult<IReadOnlyList<PizzaSpecial>>(pizzaSpecials);
        }

        public Task<IReadOnlyList<Topping>> GetToppingsAsync()
        {
            return Task.FromResult<IReadOnlyList<Topping>>(toppings);
        }

        public Task<int> PlaceOrderAsync(Order order)
        {
            order.CreatedTime = DateTime.Now;
            order.DeliveryLocation = new LatLong(51.5001, -0.1239);

            // order.UserId = GetUserId();

            var orderId = orderIdSequence++;
            order.OrderId = orderId;

            orders.Add(order);
            UpdateStatusForOrder(order);

            return Task.FromResult(order.OrderId);
        }
        
        internal void SetOrderStatusAsDelivered(int orderId)
        {
            var order = orders.Single(x => x.OrderId == orderId);

            order.CreatedTime = DateTime.Now
                .Subtract(OrderWithStatus.PreparationDuration)
                .Subtract(OrderWithStatus.DeliveryDuration);

            UpdateStatusForOrder(order);
        }

        private void UpdateStatusForOrder(Order order)
        {
            var newStatus = OrderWithStatus.FromOrder(order);

            var existing = orderWithStatuses.Find(x => x.Order.OrderId == newStatus.Order.OrderId);                        
            if(existing is not null)
            {
                orderWithStatuses.Remove(existing);
            }
            orderWithStatuses.Add(newStatus);

            var existingOrderStatusUpdated = orderStatusUpdated;
            orderStatusUpdated = new TaskCompletionSource<OrderWithStatus>();
            existingOrderStatusUpdated.SetResult(newStatus);
        }
    }
}
