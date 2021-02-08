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
        internal TaskCompletionSource<IReadOnlyList<PizzaSpecial>> PizzaSpecialTask { get; } 
            = new TaskCompletionSource<IReadOnlyList<PizzaSpecial>>();

        internal TaskCompletionSource<IReadOnlyList<Topping>> ToppingTask { get; } 
            = new TaskCompletionSource<IReadOnlyList<Topping>>();

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync() 
            => PizzaSpecialTask.Task;

        public Task<IReadOnlyList<Topping>> GetToppingsAsync()
            => ToppingTask.Task;
    }
}
