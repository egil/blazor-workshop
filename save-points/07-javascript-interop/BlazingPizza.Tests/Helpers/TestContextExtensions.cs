using System.Linq;
using BlazingPizza.Client;
using BlazingPizza.Client.Services;
using BlazingPizza.Helpers;
using BlazingPizza.Services;
using BlazingPizza.TestDoubles;
using Bunit;
using Bunit.JSInterop;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPizza
{
    public static class TestContextExtensions
    {
        public static TestContext AddBlazingPizzaSupport(this TestContext context)
        {
            var fakeAuth = context.AddTestAuthorization();
            context.Services.AddSingleton<TestAuthorizationContext>(fakeAuth);
            context.Services.AddSingleton<SignOutSessionStateManager>();
            context.JSInterop.SetupVoid(
                    "sessionStorage.setItem",
                    inv => string.Equals(inv.Arguments.FirstOrDefault(), "Microsoft.AspNetCore.Components.WebAssembly.Authentication.SignOutState")
                ).SetVoidResult();
            context.Services.AddSingleton(typeof(IRemoteAuthenticationService<>), typeof(DummyRemoteAuthenticationService<>));

            context.Services.AddSingleton<IAuthenticationViewComponentTypeProvider>(
                new AuthenticationViewComponentTypeProvider(typeof(TestAuthenticationViewComponent)));

            context.Services.AddSingleton<FakePizzaApi>(new FakePizzaApi(fakeAuth));
            context.Services.AddSingleton<IPizzaApi>(s => s.GetRequiredService<FakePizzaApi>());
            context.Services.AddSingleton<OrderState>();

            context.Services.AddSingleton<INavigationInterception, DummyNavigationInterception>();

            return context;
        }
    }
}
