namespace LIBChallanAPIs.DTOs
{
    public class UserTypeCreateDto
    {
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
