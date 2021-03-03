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
            context.Services.AddSingleton<FakeNavigationManager>();
            context.Services.AddSingleton<NavigationManager>(s => s.GetRequiredService<FakeNavigationManager>());
            context.Services.AddSingleton<OrderState>();

            return context;
        }
    }
}
