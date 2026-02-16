using LIBChallanAPIs.Models;
using System.ComponentModel.DataAnnotations;

public class AddressMaster : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string? AddressId { get; set; }

    public string? EntityId { get; set; }
    public string? WarehouseId { get; set; }

    [Required]
    [MaxLength(10)]
    public string AddressTypeId { get; set; } = string.Empty;

    [MaxLength(500)]
    public string AddressLine1 { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? AddressLine2 { get; set; }

    public string? CityId { get; set; }

    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public virtual EntityType? AddressType { get; set; }
    public virtual CityMaster? City { get; set; }
    public virtual EntityMaster? Entity { get; set; }
    public virtual Warehouse? Warehouse { get; set; }
}
