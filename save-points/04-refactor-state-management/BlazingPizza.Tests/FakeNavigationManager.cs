using Bunit;
using Bunit.Rendering;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza
{
    public class FakeNavigationManager : NavigationManager
    {
        private readonly TestContext context;

        public FakeNavigationManager(TestContext context)
        {
            this.context = context;            
            Initialize("http://localhost/", "http://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Uri = ToAbsoluteUri(uri).ToString();

            context.Renderer.Dispatcher.InvokeAsync(
                () => NotifyLocationChanged(isInterceptedLink: false));
        }
    }
}
