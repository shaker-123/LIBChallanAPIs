using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class CountryMaster : BaseEntity
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        [MaxLength(10)]
        public string CountryId { get; set; } = string.Empty; 

        [Required]
        public string CountryName { get; set; } = string.Empty;

        public string? Continent { get; set; }
        public string? PhoneCode { get; set; }
        public string? CurrencyCode { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<StateMaster>? States { get; set; }
    }
}
