using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Services
{
    public interface IAuthenticationViewComponentTypeProvider
    {
        public Type GetAuthenticationViewComponentType();
    }
}
