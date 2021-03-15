using BlazingPizza.Client.Services;
using BlazingPizza.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient(
                PizzaApi.AuthenticatedClientName,                
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            ).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient(
                PizzaApi.UnauthenticatedClientName,
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            );

            builder.Services.AddScoped<IPizzaApi, PizzaApi>();
            builder.Services.AddScoped<OrderState>();

            // Add auth services
            builder.Services.AddApiAuthorization<PizzaAuthenticationState>(options =>
            {
                options.AuthenticationPaths.LogOutSucceededPath = "";
            });

            builder.Services.AddSingleton<IAuthenticationViewComponentTypeProvider>(
                new AuthenticationViewComponentTypeProvider(
                    typeof(RemoteAuthenticatorViewCore<PizzaAuthenticationState>)));

            await builder.Build().RunAsync();
        }
    }
}
