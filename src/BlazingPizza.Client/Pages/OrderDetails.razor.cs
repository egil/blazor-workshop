using System;
using System.Threading;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Pages
{
    public partial class OrderDetails : ComponentBase, IDisposable
    {
        private readonly CancellationTokenSource pollingCancellationToken = new CancellationTokenSource();
        private OrderWithStatus? orderWithStatus;
        private bool invalidOrder;

        [Inject] private IPizzaApi API { get; set; }

        [Parameter] public int OrderId { get; set; }

        protected override void OnParametersSet()
        {
            // Start a new poll loop
            PollForUpdates();
        }

        private async void PollForUpdates()
        {
            try
            {
                await foreach (var ows in API.GetOrderUpdatesById(OrderId, pollingCancellationToken.Token))
                {
                    orderWithStatus = ows;

                    StateHasChanged();

                    if (orderWithStatus.IsDelivered)
                    {
                        break;
                    }
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                pollingCancellationToken.Cancel();
                ex.Redirect();
            }
            catch (OperationCanceledException) { }
            catch
            {
                invalidOrder = true;
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            pollingCancellationToken.Cancel();
        }
    }
}
