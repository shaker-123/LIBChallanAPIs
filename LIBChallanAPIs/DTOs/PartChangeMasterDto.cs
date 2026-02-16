namespace LIBChallanAPIs.DTOs
{
    public class PartChangeMasterDto
    {
        public int Id { get; set; }
        public string PartId { get; set; } = string.Empty;

        public string PartName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
