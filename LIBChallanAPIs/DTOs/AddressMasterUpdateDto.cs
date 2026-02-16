namespace LIBChallanAPIs.DTOs
{
    public class AddressMasterUpdateDto
    {
        public string? EntityId { get; set; }
        public string? WarehouseId { get; set; }
        public string? AddressTypeId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? CityId { get; set; }
        public string? StateId { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsActive { get; set; }
    }
}
