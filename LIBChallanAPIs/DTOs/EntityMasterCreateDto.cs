using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.DTOs
{
    public class EntityMasterCreateDto
    {
        [Required]
        public string EntityName { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string EntityTypeId { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
