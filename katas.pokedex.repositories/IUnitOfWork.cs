namespace katas.pokedex.repositories
{
    public interface IUnitOfWork
    {
        IPokemonRepository Pokemons { get; }
        void Commit();
        void Rollback();
        void ResetTransaction();
    }
}