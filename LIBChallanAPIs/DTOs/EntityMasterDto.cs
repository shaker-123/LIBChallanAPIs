namespace LIBChallanAPIs.DTOs
{
    public class EntityMasterDto
    {
        public int Id { get; set; }
        public string? EntityId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
