namespace katas.pokedex.webapi
{
    public class PaginationResult<T> 
    {

        /// <summary>
        /// Get the page number
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Get the page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Get the list of results
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Get the total items returnes
        /// </summary>
        public long ItemsCount { get; }

        /// <summary>
        /// Get the total items in the database
        /// </summary>
        public long TotalItems { get; }

        /// <summary>
        /// Gets the total count of pages
        /// </summary>
        public long TotalPages { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="items"></param>
        /// <param name="sortCriteria"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalItems"></param>
        public PaginationResult(int pageNumber, int pageSize, IEnumerable<T> items, long totalItems)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Items = items;
            this.ItemsCount = items.Count();
            this.TotalItems = totalItems;
            this.TotalPages = (this.TotalItems + this.PageSize - 1) / this.PageSize;
        }
    }
}
