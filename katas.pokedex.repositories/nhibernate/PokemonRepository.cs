using apsys.repository.nhibernate.core;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.repositories.nhibernate
{
    public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(ISession session) 
            : base(session)
        {
        }

        public void Delete()
        {
            ISQLQuery query = this._session.CreateSQLQuery("DELETE FROM pokemons");
            query.ExecuteUpdate();
        }

        public IEnumerable<Pokemon> GetPaginated(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
