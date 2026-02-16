namespace LIBChallanAPIs.DTOs
{
    public class DefectDetailsDto
    {
        public int Id { get; set; }
        public string DefectTypeId { get; set; } = string.Empty;

        public string DefectName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
