using System;
using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using static Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers;

namespace BlazingPizza.Client.Shared
{
    public class ConfigurableAuthenticationView : ComponentBase
    {
        [Inject] private IAuthenticationViewComponentTypeProvider AuthViewComponentTypeProvider { get; set; }

        [Parameter] public string Action { get; set; }

        [Parameter] public PizzaAuthenticationState AuthenticationState { get; set; }

        [Parameter] public EventCallback<PizzaAuthenticationState> OnLogInSucceeded { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent(0, AuthViewComponentTypeProvider.GetAuthenticationViewComponentType());
            builder.AddAttribute(1, nameof(Action), Action);
            builder.AddAttribute(4, "AuthenticationState", AuthenticationState);
            builder.AddAttribute(5, "OnLogInSucceeded", OnLogInSucceeded);            
            builder.CloseComponent();
        }
    }
}
