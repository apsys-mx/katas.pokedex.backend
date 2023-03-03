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
    public static class CreatePokemonService
    {

        /// <summary>
        /// Command class
        /// </summary>
        public class Command : IRequest<Pokemon>
        {
            public Command(string name)
            {
                Name = name;
            }
            public string Name { get; }
        }


        public class Handler : IRequestHandler<Command, Pokemon>
        {

            private readonly IUnitOfWork uoW;

            public Handler(IUnitOfWork uoW)
            {
                this.uoW = uoW;
            }

            public Task<Pokemon> Handle(Command request, CancellationToken cancellationToken)
            {
                using HttpClient client = new HttpClient();
                var result = client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{request.Name}").Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    var pokemontsResult = JsonConvert.DeserializeObject<dynamic>(content);
                    var name = pokemontsResult.name.ToString();
                    var code = int.Parse(pokemontsResult.id.ToString());
                    var existingPokemon = this.uoW.Pokemons.GetByCode(code);
                    if (existingPokemon != null)
                        return Task.FromResult<Pokemon>(null);
                    existingPokemon = this.uoW.Pokemons.Add(code, name);
                    this.uoW.Commit();
                    return Task.FromResult<Pokemon>(existingPokemon);
                }
                else {
                    throw new Exception();
                }
                
            }
        }
    }
}
