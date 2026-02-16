public class AddressMasterCreateDto
{
    public string? EntityId { get; set; }
    public string? WarehouseId { get; set; }
    public string? AddressTypeId { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string? CityId { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}