using System;
using System.Threading;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
    public partial class OrderDetails : ComponentBase
    {
        private OrderWithStatus? orderWithStatus;
        private bool invalidOrder;
        private CancellationTokenSource? pollingCancellationToken;

        [Inject] private IPizzaApi API { get; set; }

        [Parameter] public int OrderId { get; set; }

        protected override void OnParametersSet()
        {
            // If we were already polling for a different order, stop doing so
            pollingCancellationToken?.Cancel();

            // Start a new poll loop
            PollForUpdates();
        }

        private async void PollForUpdates()
        {
            pollingCancellationToken = new CancellationTokenSource();

            try
            {
                await foreach (var ows in API.GetOrderUpdatesById(OrderId, pollingCancellationToken.Token))
                {
                    orderWithStatus = ows;

                    StateHasChanged();

                    if (orderWithStatus.IsDelivered)
                    {
                        pollingCancellationToken.Cancel();
                    }
                }
            }
            catch (Exception ex)
            {
                invalidOrder = true;
                pollingCancellationToken.Cancel();
                Console.Error.WriteLine(ex);
                StateHasChanged();
            }
        }
    }
}
