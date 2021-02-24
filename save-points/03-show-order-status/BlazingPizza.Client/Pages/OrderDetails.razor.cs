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
                await foreach (var ows in API.GetOrderUpdatesById(OrderId))
                {
                    orderWithStatus = ows;

                    StateHasChanged();

                    if (orderWithStatus.IsDelivered)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                invalidOrder = true;
                Console.Error.WriteLine(ex.Message);
                StateHasChanged();
            }
        }
    }
}
