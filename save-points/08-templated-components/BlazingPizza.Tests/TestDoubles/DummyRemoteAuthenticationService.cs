using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.TestDoubles
{
    public class DummyRemoteAuthenticationService<TRemoteAuthenticationState> : IRemoteAuthenticationService<TRemoteAuthenticationState>
        where TRemoteAuthenticationState : RemoteAuthenticationState
    {
        public Task<RemoteAuthenticationResult<TRemoteAuthenticationState>> CompleteSignInAsync(RemoteAuthenticationContext<TRemoteAuthenticationState> context)
        {
            throw new NotImplementedException("CompleteSignInAsync");
        }

        public Task<RemoteAuthenticationResult<TRemoteAuthenticationState>> CompleteSignOutAsync(RemoteAuthenticationContext<TRemoteAuthenticationState> context)
        {
            throw new NotImplementedException("CompleteSignOutAsync");
        }

        public Task<RemoteAuthenticationResult<TRemoteAuthenticationState>> SignInAsync(RemoteAuthenticationContext<TRemoteAuthenticationState> context)
        {
            throw new NotImplementedException("SignInAsync");
        }

        public Task<RemoteAuthenticationResult<TRemoteAuthenticationState>> SignOutAsync(RemoteAuthenticationContext<TRemoteAuthenticationState> context)
        {
            throw new NotImplementedException("SignOutAsync");
        }
    }
}
