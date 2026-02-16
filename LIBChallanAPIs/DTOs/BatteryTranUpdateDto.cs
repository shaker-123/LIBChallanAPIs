namespace LIBChallanAPIs.DTOs
{
    public class BatteryTranUpdateDto
    {
        public string? BatteryTransId { get; set; } // null = new battery
        public string BatterySerial { get; set; } = string.Empty;
        public string? BatteryIdInLIBSystem { get; set; }
        public string? Barcode { get; set; }
        public string CurrentStatusId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
