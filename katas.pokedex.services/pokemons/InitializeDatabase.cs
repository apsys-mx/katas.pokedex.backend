using katas.pokedex.repositories;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.services.pokemons
{
    public static class InitializeDatabase
    {
        /// <summary>
        /// Command class
        /// </summary>
        public class Command : IRequest
        {
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork uoW;

            public Handler(IUnitOfWork uoW)
            {
                this.uoW = uoW;
            }

            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                using HttpClient client = new HttpClient();
                var result = client.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=500&offset=0").Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    var pokemontsResult = JsonConvert.DeserializeObject<dynamic>(content);
                    var results = JsonConvert.SerializeObject(pokemontsResult.results);
                    var pokemonts = JsonConvert.DeserializeObject<IEnumerable< dynamic>>(results);

                    this.uoW.Pokemons.Delete();

                    foreach (var pokemon in pokemonts)
                    {
                        var name = pokemon.name.ToString();
                        string url = pokemon.url.ToString();
                        var code = int.Parse(url.Replace("https://pokeapi.co/api/v2/pokemon/", "").Replace("/", ""));
                        var pokemonToInsert = new Pokemon(code, name);
                        pokemonToInsert.Id = Guid.NewGuid().ToString();
                        this.uoW.Pokemons.Add(pokemonToInsert);
                    }

                    this.uoW.Commit();
                }
                return Task.CompletedTask;
            }
        }
    }
}
