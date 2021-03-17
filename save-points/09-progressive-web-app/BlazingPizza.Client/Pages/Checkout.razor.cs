using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout : ComponentBase
    {
        private bool IsSubmitting { get; set; }

        [Inject] private IPizzaApi Api { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private OrderState OrderState { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            // In the background, ask if they want to be notified about order updates
            _ = RequestNotificationSubscriptionAsync();
        }

        async Task RequestNotificationSubscriptionAsync()
        {
            var subscription = await JSRuntime.InvokeAsync<NotificationSubscription>("blazorPushNotifications.requestSubscription");
            
            if (subscription is not null)
            {
                try
                {
                    await Api.SubscribeToNotifications(subscription);
                }
                catch (AccessTokenNotAvailableException ex)
                {
                    ex.Redirect();
                }
            }
        }

        private async Task PlaceOrder()
        {
            IsSubmitting = true;

            try
            {
                var orderId = await Api.PlaceOrderAsync(OrderState.Order);
                IsSubmitting = false;
                OrderState.ResetOrder();
                NavigationManager.NavigateTo($"myorders/{orderId}");
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }
    }
}
