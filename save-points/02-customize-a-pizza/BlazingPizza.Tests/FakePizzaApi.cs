using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;

namespace BlazingPizza
{
    public class FakePizzaApi : IPizzaApi
    {
        private readonly IReadOnlyList<PizzaSpecial> pizzas;

        public FakePizzaApi(IEnumerable<PizzaSpecial> pizzas) => this.pizzas = pizzas.ToArray();

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync() => Task.FromResult(pizzas);
    }
}
