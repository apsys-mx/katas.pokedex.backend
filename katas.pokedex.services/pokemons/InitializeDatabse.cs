using katas.pokedex.repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.services.pokemons
{
    public static class InitializeDatabse
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
                throw new NotImplementedException();
            }
        }
    }
}
