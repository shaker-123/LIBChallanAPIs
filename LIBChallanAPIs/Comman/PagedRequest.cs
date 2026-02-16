namespace LIBChallanAPIs.Comman
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SearchColumn { get; set; }
        public string? SearchValue { get; set; }

        public string? SortColumn { get; set; }
        public string SortDirection { get; set; } = "ASC";

        public RecordStatus Status { get; set; } = RecordStatus.All;

        public int Skip => (PageNumber - 1) * PageSize;
    }

    public enum RecordStatus
    {
        All = 0,
        Active = 1,
        Inactive = 2
    }

}
