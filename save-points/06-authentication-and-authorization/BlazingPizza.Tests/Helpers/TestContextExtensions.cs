using System.Linq;
using BlazingPizza.Client;
using BlazingPizza.Client.Services;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
            
            var fakeAuth = context.AddTestAuthorization();
            context.Services.AddSingleton<TestAuthorizationContext>(fakeAuth);
            context.Services.AddSingleton<SignOutSessionStateManager>();
            context.JSInterop.SetupVoid(
                    "sessionStorage.setItem",
                    inv => string.Equals(inv.Arguments.FirstOrDefault(), "Microsoft.AspNetCore.Components.WebAssembly.Authentication.SignOutState")
                ).SetVoidResult();

            return context;
        }
    }
}
