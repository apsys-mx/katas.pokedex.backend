using MediatR;
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
        public class Command : IRequest<IEnumerable<Pokemon>>
        {
            public Command(string code)
            {
                Code = code;
            }
            public string Code { get; }
        }


        public class Handler : IRequestHandler<Command, IEnumerable<Pokemon>>
        {
            public Task<IEnumerable<Pokemon>> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
