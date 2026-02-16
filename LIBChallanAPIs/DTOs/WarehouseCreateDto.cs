using System.ComponentModel.DataAnnotations;

public class WarehouseCreateDto
{

    [Required]
    public string? CityId { get; set; }
    [Required]
    public string? EntityId { get; set; }
    public bool IsActive { get; set; } = true;
}