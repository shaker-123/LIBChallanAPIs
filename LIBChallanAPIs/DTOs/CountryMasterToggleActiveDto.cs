using System.ComponentModel.DataAnnotations;

namespace LIBChallanAPIs.DTOs
{
    public class CountryMasterToggleActiveDto
    {
        public int CountryId { get; set; }

        public bool IsActive { get; set; }
    }
}
