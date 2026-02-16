namespace LIBChallanAPIs.DTOs
{
    public class DefectDetailsCreateDto
    {
        public string DefectCode { get; set; } = string.Empty;
        public string DefectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
