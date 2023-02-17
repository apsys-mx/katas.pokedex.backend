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
    }
}
