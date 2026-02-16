namespace LIBChallanAPIs.DTOs
{
    public class AddressTypeDto
    {
        public int Id { get; set; }
        public string? AddressTypeId { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

}
