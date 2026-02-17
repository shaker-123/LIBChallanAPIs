using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.DTOs
{
    public class EntityMasterUpdateDto
    {
        public string? EntityName { get; set; }
        public string EntityTypeId { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? IsActive { get; set; }
    }
}
