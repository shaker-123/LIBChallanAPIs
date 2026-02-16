public class PartChangeMasterCreateDto
{
    public string PartName { get; set; } = null!;
    public string? PartDescription { get; set; }
    public bool IsActive { get; set; } = true;
}