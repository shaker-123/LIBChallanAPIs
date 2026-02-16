using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class GSTTypeMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string GSTTypeId { get; set; } = string.Empty;

        public string GSTTypeCode { get; set; } = string.Empty;

        public string GSTTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<GSTMaster> GSTMasters { get; set; } = new List<GSTMaster>();
    }
}
