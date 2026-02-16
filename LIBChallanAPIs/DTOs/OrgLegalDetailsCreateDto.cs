using System.ComponentModel.DataAnnotations;

public class OrgLegalDetailsCreateDto
{
    [Required]
    public string? EntityId { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 15)]
    public string GSTINNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(21, MinimumLength = 21)]
    public string CINNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string PANNumber { get; set; } = string.Empty;

    [Required]
    public string? CityId { get; set; }

    public bool IsActive { get; set; } = true;
}
