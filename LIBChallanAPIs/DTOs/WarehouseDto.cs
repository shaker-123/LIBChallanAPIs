using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.DTOs
{
    public class WarehouseDto
    {
        public string? WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = string.Empty;
        public string? CityId { get; set; }
        public string? EntityId { get; set; }
        public bool IsActive { get; set; }
    }
}
