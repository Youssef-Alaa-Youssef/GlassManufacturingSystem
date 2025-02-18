namespace Factory.PL.ViewModels
{
    public class PaginationViewModel<T>
    {
        public List<T> ActiveUsers { get; set; } = new List<T>();
        public List<T> LockedUsers { get; set; } = new List<T>();
        public string Query { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
