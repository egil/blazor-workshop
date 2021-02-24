using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
    public partial class Index : ComponentBase
    {
        private IReadOnlyList<PizzaSpecial> specials;

        [Inject] private IPizzaApi Api { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private OrderState OrderState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            specials = await Api.GetPizzaSpecialsAsync();
        }

        private async Task PlaceOrder()
        {
            var orderId = await Api.PlaceOrderAsync(OrderState.Order);
            OrderState.ResetOrder();
            NavigationManager.NavigateTo($"myorders/{orderId}");
        }
    }
}
