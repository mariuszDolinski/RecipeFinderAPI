namespace RecipeFinderAPI.Models
{
    public class PaginationResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int TotalCount { get; set; }

        public PaginationResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalCount = totalCount;
            From = pageSize * (pageNumber - 1) + 1;
            To = From + pageSize - 1;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }
    }
}
