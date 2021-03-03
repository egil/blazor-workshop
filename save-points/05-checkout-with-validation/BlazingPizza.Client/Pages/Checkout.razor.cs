using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout : ComponentBase
    {
        [Inject] private IPizzaApi Api { get; set; }
        
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private OrderState OrderState { get; set; }

        private async Task PlaceOrder()
        {
            var orderId = await Api.PlaceOrderAsync(OrderState.Order);
            OrderState.ResetOrder();
            NavigationManager.NavigateTo($"myorders/{orderId}");
        }
    }
}
