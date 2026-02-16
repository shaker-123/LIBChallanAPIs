namespace LIBChallanAPIs.DTOs
{
    public class CityMasterCreateDto
    {
        public string CityCode { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public string? PostalCode { get; set; }
        public string? AreaCode { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
