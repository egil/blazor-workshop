using BlazingPizza.Client.Services;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPizza
{
    public static class TestContextExtensions
    {
        public static FakeNavigationManager AddFakeNavigationManager(this TestContext context)
        {
            var result = new FakeNavigationManager(context);
            context.Services.AddSingleton<NavigationManager>(result);
            return result;
        }

        public static FakePizzaApi AddFakePizzaApi(this TestContext context)
        {
            var result = new FakePizzaApi();
            context.Services.AddSingleton<IPizzaApi>(result);
            return result;
        }
    }
}
