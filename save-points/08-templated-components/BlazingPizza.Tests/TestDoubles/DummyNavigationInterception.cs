using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazingPizza.TestDoubles
{
    public class DummyNavigationInterception : INavigationInterception
    {
        public Task EnableNavigationInterceptionAsync()
        {
            return Task.CompletedTask;
        }
    }
}
