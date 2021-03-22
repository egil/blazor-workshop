using System;

namespace BlazingPizza.Services
{
    public class AuthenticationViewComponentTypeProvider : IAuthenticationViewComponentTypeProvider
    {
        private readonly Type authComponentType;

        public AuthenticationViewComponentTypeProvider(Type authComponentType)
        {
            this.authComponentType = authComponentType;
        }

        public Type GetAuthenticationViewComponentType()
        {
            return authComponentType;
        }
    }
}
