using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public static class PizzaApiExtensions
    {
        public static async Task<OrderWithStatus> GetOrderWithStatusById(this IPizzaApi api, int orderId)
        {
            return (await api.GetOrdersWithStatusAsync()).Single(x => x.Order.OrderId == orderId);
        }

        public static async Task<Order> GetOrderById(this IPizzaApi api, int orderId)
        {
            return (await api.GetOrdersWithStatusAsync()).Single(x => x.Order.OrderId == orderId).Order;
        }
    }
}
