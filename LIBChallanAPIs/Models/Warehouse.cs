using LIBChallanAPIs.Models;
using System.ComponentModel.DataAnnotations;

public class Warehouse : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string? WarehouseId { get; set; }

    public string? WarehouseCode { get; set; }

    [Required]
    [MaxLength(10)]
    public string? CityId { get; set; }  

    [Required]
    [MaxLength(10)]
    public string? EntityId { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual EntityMaster? EntityM { get; set; }
    public virtual CityMaster? CityM { get; set; }
}
