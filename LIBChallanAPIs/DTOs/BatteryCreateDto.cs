namespace LIBChallanAPIs.DTOs
{
    public class BatteryCreateDto
    {
        public string BatterySerial { get; set; } = string.Empty;
        public string CurrentStatusId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;

        public string Barcode { get; set; } = null!;
        public string BatteryIdInLIBSystem { get; set; } = null!;
    }
}
