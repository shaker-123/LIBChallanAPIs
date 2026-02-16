using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBChallanAPIs.Models
{
    public class CorrectiveAction : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionId { get; set; } = string.Empty;


        public string ActionName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
