using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex
{

    [Migration(1)]
    public class M001_CreatePokemonsTable : Migration
    {
        public override void Down()
        {
            Delete.Table("pokemons");
        }

        public override void Up()
        {
            Create.Table("pokemons")
                .WithColumn("id").AsString().PrimaryKey()
                .WithColumn("code").AsInt32().NotNullable()
                .WithColumn("name").AsString().NotNullable();
        }
    }
}
