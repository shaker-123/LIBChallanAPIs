namespace LIBChallanAPIs.DTOs
{
    public class StateMasterCreateDto
    {
        public string StateName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string CountryId { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string? GstCode { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
