namespace LIBChallanAPIs.DTOs
{
    public class BatteryStatusDto
    {
        public int Id { get; set; }

        public string StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

}
