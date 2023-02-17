using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.services.pokemons
{
    public static class GetPokemonByName
    {
        /// <summary>
        /// Command class
        /// </summary>
        public class Command : IRequest<ExpandoObject>
        {
            public Command(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        public class Handler : IRequestHandler<Command, ExpandoObject>
        {
            public Task<ExpandoObject> Handle(Command request, CancellationToken cancellationToken)
            {
                using HttpClient client = new HttpClient();
                var result = client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{request.Name}").Result;
                var content = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var pokemontsResult = JsonConvert.DeserializeObject<ExpandoObject>(content);
                    return Task.FromResult(pokemontsResult);
                }
                else
                    return Task.FromResult<ExpandoObject>(null);
            }
        }
    }
}
