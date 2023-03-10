using apsys.repository.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.repositories
{
    public interface IPokemonRepository : IRepository<Pokemon>
    {
        void Delete();
        IEnumerable<Pokemon> GetPaginated(int pageNumber, int pageSize);
        Pokemon GetByCode(int code);
        Pokemon Add(int code, string name);
    }
}
