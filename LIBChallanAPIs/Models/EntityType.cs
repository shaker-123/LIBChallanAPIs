using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class EntityType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string? AddressTypeId { get; set; }  

        public string TypeCode { get; set; } = string.Empty;

        public string TypeName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
