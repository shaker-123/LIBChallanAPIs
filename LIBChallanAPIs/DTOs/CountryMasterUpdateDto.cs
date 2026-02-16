using System.ComponentModel.DataAnnotations;

public class CountryMasterUpdateDto
{

    public string CountryName { get; set; } = string.Empty;

    public string? Continent { get; set; }

    public string? PhoneCode { get; set; }

    public string? CurrencyCode { get; set; }
}
