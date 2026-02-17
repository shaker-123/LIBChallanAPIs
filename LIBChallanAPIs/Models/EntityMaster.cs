using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class EntityMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string EntityId { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string? EntityName { get; set; }

        [Required]
        [MaxLength(10)]
        public string EntityTypeId { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? ContactPerson { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Mobile { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("EntityTypeId")]
        public virtual EntityType? Entity { get; set; }

        public virtual ICollection<Warehouse>? Warehouses { get; set; }
        public virtual ICollection<AddressMaster>? Addresses { get; set; }
    }
}
