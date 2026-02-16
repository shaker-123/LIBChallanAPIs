namespace LIBChallanAPIs.DTOs
{
    public class CityMasterDto
    {
        public int Id { get; set; }
        public string CityId { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public string? PostalCode { get; set; }
        public string? AreaCode { get; set; }
        public bool IsActive { get; set; }
    }
}
