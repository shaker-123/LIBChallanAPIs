using System.ComponentModel.DataAnnotations;

public class OrgLegalDetailsUpdateDto
{
    [StringLength(15, MinimumLength = 15)]
    public string? GSTINNumber { get; set; }

    [StringLength(21, MinimumLength = 21)]
    public string? CINNumber { get; set; }

    [StringLength(10, MinimumLength = 10)]
    public string? PANNumber { get; set; }

    public string? CityId { get; set; }
}
