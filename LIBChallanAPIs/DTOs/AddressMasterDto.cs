namespace LIBChallanAPIs.DTOs
{
    public class AddressMasterDto
    {
        public int Id { get; set; }
        public string? AddressId { get; set; }
        public string? EntityId { get; set; }
        public string? WarehouseId { get; set; }
        public string? AddressTypeId { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string? CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

}
