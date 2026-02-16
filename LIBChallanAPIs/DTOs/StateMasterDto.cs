namespace LIBChallanAPIs.DTOs
{
    public class StateMasterDto
    {
        public int Id { get; set; }

        public string StateId { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string? CountryId { get; set; }
        public string? Region { get; set; }
        public string? GstCode { get; set; }
        public bool IsActive { get; set; }
    }
}
