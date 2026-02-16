public class OrgLegalDetailsDto
{
    public int Id { get; set; }
    public string? LegalId { get; set; }
    public string? EntityId { get; set; }
    public string GSTINNumber { get; set; } = string.Empty;
    public string CINNumber { get; set; } = string.Empty;
    public string PANNumber { get; set; } = string.Empty;
    public string? CityId { get; set; }
    public bool IsActive { get; set; }
}
