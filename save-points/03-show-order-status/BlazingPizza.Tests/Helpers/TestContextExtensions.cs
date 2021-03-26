using BlazingPizza.Client;
using BlazingPizza.Client.Services;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPizza
{
    public static class TestContextExtensions
    {
        public static TestContext AddBlazingPizzaSupport(this TestContext context)
        {
            context.Services.AddSingleton<FakePizzaApi>(new FakePizzaApi());
            context.Services.AddSingleton<IPizzaApi>(s => s.GetRequiredService<FakePizzaApi>());

            return context;
        }
    }
}
