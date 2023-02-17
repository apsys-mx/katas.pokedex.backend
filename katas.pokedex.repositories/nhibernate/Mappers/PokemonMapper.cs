using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.repositories.nhibernate.Mappers
{
    public class PokemonMapper: ClassMapping<Pokemon>
    {
        public PokemonMapper()
        {
            Table("pokemons");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Assigned);
                x.Column("id");
            });
            Property(x => x.Code, x => { x.Column("code"); });
            Property(x => x.Name, x => { x.Column("name"); });
        }
    }
}
