using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class BatteryTran : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string BatteryTransId { get; set; }

        [Required, MaxLength(10)]
        public string ActivityId { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string CurrentStatusId { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string CustomerId { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string WarehouseId { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string BatterySerial { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? BatteryIdInLIBSystem { get; set; }

        [MaxLength(50)]
        public string? Barcode { get; set; }

        public bool IsActive { get; set; } = true;

        [Required, MaxLength(10)]
        public string FirmwareStatusId { get; set; } = string.Empty;


        [MaxLength(10)]
        public string? CorrectiveActionId { get; set; }

        [MaxLength(10)]
        public string? DefectTypeId { get; set; }

        [MaxLength(10)]
        public string? PartId { get; set; }


        [ForeignKey(nameof(ActivityId))]
        public virtual ServiceActivity? Activity { get; set; }

        [ForeignKey(nameof(CurrentStatusId))]
        public virtual BatteryStatus? BatteryStatus { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual EntityMaster? Customer { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public virtual Warehouse? Warehouse { get; set; }

        [ForeignKey(nameof(FirmwareStatusId))]
        public virtual FirmwareStatus? FirmwareStatus { get; set; }

        [ForeignKey(nameof(CorrectiveActionId))]
        public virtual CorrectiveAction? CorrectiveAction { get; set; }

        [ForeignKey(nameof(DefectTypeId))]
        public virtual DefectDetail? DefectDetail { get; set; }

        [ForeignKey(nameof(PartId))]
        public virtual PartChangeMaster? PartChangeMaster { get; set; }
    }

}
