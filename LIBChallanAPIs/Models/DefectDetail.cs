using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class DefectDetail : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string DefectTypeId { get; set; } = string.Empty;


        public string DefectName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
