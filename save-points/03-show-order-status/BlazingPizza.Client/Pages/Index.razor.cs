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
        private Pizza configuringPizza;
        private bool showingConfigureDialog;
        private Order order = new Order();

        [Inject] private IPizzaApi Api { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            specials = await Api.GetPizzaSpecialsAsync();
        }

        private void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            configuringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>(),
            };

            showingConfigureDialog = true;
        }

        private void CancelConfigurePizzaDialog()
        {
            showingConfigureDialog = false;
            configuringPizza = null;
        }

        private void ConfirmConfigurePizzaDialog()
        {
            showingConfigureDialog = false;            
            order.Pizzas.Add(configuringPizza);
            configuringPizza = null;
        }

        private async Task PlaceOrder()
        {
            var orderId = await Api.PlaceOrderAsync(order);
            order = new Order();
            NavigationManager.NavigateTo($"myorders/{orderId}");
        }
    }
}
