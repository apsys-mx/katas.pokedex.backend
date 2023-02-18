using apsys.repository.core;
using katas.pokedex.repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.services.pokemons
{
    public static class GetPokemonsServices
    {

        /// <summary>
        /// Command class
        /// </summary>
        public class Command : IRequest<GetManyAndCountResult<Pokemon>>
        {

            public Command(int pageNumber, int pageSize)
            {
                PageNumber = pageNumber;
                PageSize = pageSize;
            }

            public int PageNumber { get; }
            public int PageSize { get; }
        }

        /// <summary>
        /// Handler class
        /// </summary>
        public class Handler : IRequestHandler<Command, GetManyAndCountResult<Pokemon>>
        {
            private readonly IUnitOfWork uoW;

            public Handler(IUnitOfWork uoW)
            {
                this.uoW = uoW;
            }

            public Task<GetManyAndCountResult<Pokemon>> Handle(Command request, CancellationToken cancellationToken)
            {
                SortingCriteria sorting = new SortingCriteria("code", SortingCriteriaType.Ascending);
                var pokemons = this.uoW.Pokemons.Get(request.PageNumber, request.PageSize, sorting);
                var count = this.uoW.Pokemons.Count();
                var result = new GetManyAndCountResult<Pokemon>(pokemons, count);
                return Task.FromResult(result);
            }
        }
    }
}
