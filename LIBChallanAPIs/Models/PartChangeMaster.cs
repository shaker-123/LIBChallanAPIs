using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class PartChangeMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? PartId {  get; set; }
        public string PartName { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
