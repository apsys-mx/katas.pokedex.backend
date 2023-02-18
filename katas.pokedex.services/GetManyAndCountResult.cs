namespace katas.pokedex.services
{
    /// <summary>
    /// Get many and count result for paginated evaluations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetManyAndCountResult<T>
    {
        public GetManyAndCountResult(IEnumerable<T> items, long count)
        {
            Items = items;
            Count = count;
        }

        public IEnumerable<T> Items { get; }
        public long Count { get; }
    }
}