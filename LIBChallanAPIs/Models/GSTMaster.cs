using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class GSTMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string GSTId { get; set; } = string.Empty;

        public string GSTSlabId { get; set; } = string.Empty; 
        public string GSTTypeId { get; set; } = string.Empty; 

        public decimal GSTPercentage { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual GSTTypeMaster GSTType { get; set; }
        public virtual GSTSlabMaster GSTSlab { get; set; }
    }
}
