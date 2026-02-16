using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class GSTSlabMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string GSTSlabId { get; set; } = string.Empty; 

        public string SlabCode { get; set; } = string.Empty;
        public decimal TotalPercentage { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<GSTMaster> GSTDetails { get; set; } = new List<GSTMaster>();
    }
}
