namespace LIBChallanAPIs.DTOs
{
    public class UserTypeDto
    {
        public int Id { get; set; }
        public string TypeId { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

}
