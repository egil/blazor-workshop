﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public class PizzaApi : IPizzaApi
    {
        private readonly HttpClient httpClient;

        public PizzaApi(HttpClient httpClient)
            => this.httpClient = httpClient;

        public async Task<IReadOnlyList<PizzaSpecial>> GetPizzaSpecialsAsync()
            => await httpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
    }
}
