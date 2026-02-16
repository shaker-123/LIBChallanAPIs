using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class CityMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required, MaxLength(10)]
        public string CityId { get; set; } = string.Empty; 

        [Required, MaxLength(100)]
        public string CityName { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string StateId { get; set; } = string.Empty; 

        public string? PostalCode { get; set; }
        public string? AreaCode { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual StateMaster? State { get; set; }
        public virtual ICollection<Warehouse>? Warehouses { get; set; }

        public virtual ICollection<OrgLegalDetail>? OrgLegalDetails { get; set; }

        public virtual ICollection<AddressMaster>? Addresses { get; set; }
    }
}
