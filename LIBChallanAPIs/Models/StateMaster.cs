using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.Models
{
    public class StateMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required, MaxLength(10)]
        public string StateId { get; set; } = string.Empty;  

        [Required, MaxLength(100)]
        public string StateName { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string CountryId { get; set; } = string.Empty;

        public string? Region { get; set; }
        public string? GstCode { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public virtual CountryMaster? Country { get; set; }
        public virtual ICollection<CityMaster>? Cities { get; set; }
    }
}
