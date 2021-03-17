using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.ComponentsLibrary
{
    public partial class ConfigurePizzaDialog : ComponentBase
    {
        private IReadOnlyList<Topping>? toppings;

        [Inject] public IPizzaApi Api { get; set; }

        [Parameter] public Pizza Pizza { get; set; }

        [Parameter] public EventCallback OnConfirm { get; set; }

        [Parameter] public EventCallback OnCancel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            toppings = await Api.GetToppingsAsync();
        }

        void ToppingSelected(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var index) && index >= 0)
            {
                AddTopping(toppings[index]);
            }
        }

        void AddTopping(Topping topping)
        {
            var hasToppingAlready = Pizza.Toppings.Any(pt => pt.Topping == topping);
            if (!hasToppingAlready)
            {
                Pizza.Toppings.Add(new PizzaTopping() { Topping = topping });
            }
        }

        void RemoveTopping(Topping topping)
        {
            Pizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }
    }
}
