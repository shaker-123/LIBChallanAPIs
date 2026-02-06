namespace LIBChallanAPIs.Comman
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling(TotalRecords / (double)PageSize);

        public List<T> Data { get; set; } = new();
    }
}
