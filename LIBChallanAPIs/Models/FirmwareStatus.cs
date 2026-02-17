using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class FirmwareStatus : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string FirmwareStatusId { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirmwareStatusName { get; set; } = null!;

        [MaxLength(20)]
        public string BatteryType { get; set; } = "45Ah";

        public bool IsActive { get; set; } = true;
    }
}
