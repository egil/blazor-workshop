using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza
{
    public sealed class MissingAccessTokenPizzaApi : IPizzaApi
    {
        private readonly AccessTokenNotAvailableException accessTokenNotAvailableException;

        public MissingAccessTokenPizzaApi(NavigationManager navigationManager)
        {
            accessTokenNotAvailableException = new AccessTokenNotAvailableException(
                navigationManager,
                new AccessTokenResult(
                    AccessTokenResultStatus.RequiresRedirect,
                    new AccessToken(),
                    "authentication/login"),
                Array.Empty<string>());
        }

        public Task<OrderWithStatus> GetOrderAsync(int orderId)
        {
            throw accessTokenNotAvailableException;
        }

        public Task<IReadOnlyList<OrderWithStatus>> GetOrdersAsync()
        {
            throw accessTokenNotAvailableException;
        }

        public IAsyncEnumerable<OrderWithStatus> GetOrderUpdatesById(int orderId, CancellationToken cancellationToken = default)
        {
            throw accessTokenNotAvailableException;
        }

        public Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
        {
            throw accessTokenNotAvailableException;
        }

        public Task<IReadOnlyList<Topping>> GetToppingsAsync()
        {
            throw accessTokenNotAvailableException;
        }

        public Task<int> PlaceOrderAsync(Order order)
        {
            throw accessTokenNotAvailableException;
        }

        public Task SubscribeToNotifications(NotificationSubscription subscription)
        {
            throw accessTokenNotAvailableException;
        }
    }
}
