using apsys.repository.nhibernate.core;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public Pokemon Add(int code, string name)
        {
            Pokemon pokemon = new Pokemon(code, name);
            this.Add(pokemon);
            return pokemon;
        }

        public void Delete()
        {
            ISQLQuery query = this._session.CreateSQLQuery("DELETE FROM pokemons");
            query.ExecuteUpdate();
        }

        public Pokemon GetByCode(int code)
            => this.Get(x=>x.Code == code).FirstOrDefault();

        public IEnumerable<Pokemon> GetPaginated(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
