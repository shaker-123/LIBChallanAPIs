namespace LIBChallanAPIs.DTOs
{
    public class RoleDto
    {
        public string? RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
