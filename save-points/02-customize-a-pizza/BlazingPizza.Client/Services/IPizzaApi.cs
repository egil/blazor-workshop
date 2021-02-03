using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public interface IPizzaApi
    {
        Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync();
    }
}
