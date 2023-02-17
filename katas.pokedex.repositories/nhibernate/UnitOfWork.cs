using Microsoft.Extensions.Configuration;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace katas.pokedex.repositories.nhibernate
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ISession session;
        private ITransaction transaction;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sipService"></param>
        /// <param name="sapServices"></param>
        /// <param name="identityServer3Services"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UnitOfWork(ISession session, IConfiguration configuration)
        {
            this.session = session;
            this.configuration = configuration;
            this.transaction = session.BeginTransaction();
            this.Pokemons = new PokemonRepository(session);
        }

        public IPokemonRepository Pokemons { get; }

        public void Commit()
        {
            if (this.transaction != null && this.transaction.IsActive)
                this.transaction.Commit();
            else
                throw new TransactionException("The actual transaction is not longer active");
        }

        public void ResetTransaction()
        {
            this.transaction = this.session.BeginTransaction();
        }

        public void Rollback()
        {
            if (this.transaction != null && this.transaction.IsActive)
                this.transaction.Rollback();
            else
                throw new TransactionException("The actual transaction is not longer active");
        }
    }
}
