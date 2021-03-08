using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout : ComponentBase
    {
        private bool IsSubmitting { get; set; }

        [Inject] private IPizzaApi Api { get; set; }
        
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private OrderState OrderState { get; set; }

        private async Task PlaceOrder()
        {
            IsSubmitting = true;
            var orderId = await Api.PlaceOrderAsync(OrderState.Order);
            IsSubmitting = false;
            OrderState.ResetOrder();
            NavigationManager.NavigateTo($"myorders/{orderId}");
        }
    }
}
