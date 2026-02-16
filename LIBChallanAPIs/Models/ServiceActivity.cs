using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class ServiceActivity : BaseEntity
    {
        [Key]
        public int Id { get; set; } 


        [Required, MaxLength(10)]
        public string ActivityId { get; set; } = null!;

        [Required, MaxLength(10)]
        public string ActivityEngineerId { get; set; } = null!;

        [Required, MaxLength(10)]
        public string EntityId { get; set; } = null!; 

        [Required, MaxLength(10)]
        public string WarehouseId { get; set; } = null!; 


        [Required, MaxLength(10)]
        public string StatusId { get; set; } = null!;


        [Required]
        public DateTime ActivityDate { get; set; }

        public int NumberOfBatteriesOnSite { get; set; }

        public int NumberOfBatteriesOnSiteRTF { get; set; }

        public bool IsActive { get; set; } = true;


        [ForeignKey(nameof(ActivityEngineerId))]
        public virtual AppUser? ActivityEngineer { get; set; }

        [ForeignKey(nameof(EntityId))]
        public virtual EntityMaster? Entity { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public virtual Warehouse? Warehouse { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual ActivityStatus? Status { get; set; }

        public virtual ICollection<BatteryTran> Batteries { get; set; } = new List<BatteryTran>();
    }
}
