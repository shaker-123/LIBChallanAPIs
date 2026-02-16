using System.ComponentModel.DataAnnotations;

public class CountryMasterDto
{
    public int Id { get; set; }

    public string CountryId { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public string? Continent { get; set; }

    public string? PhoneCode { get; set; }

    public string? CurrencyCode { get; set; }

    public bool IsActive { get; set; } = true;
}