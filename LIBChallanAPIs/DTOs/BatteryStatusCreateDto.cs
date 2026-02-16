public class BatteryStatusCreateDto
{
    public string StatusName { get; set; } = null!;
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
}