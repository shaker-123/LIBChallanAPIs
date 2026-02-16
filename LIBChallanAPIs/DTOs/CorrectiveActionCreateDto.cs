public class CorrectiveActionCreateDto
{
    public string ActionName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}